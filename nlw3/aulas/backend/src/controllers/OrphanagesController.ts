import { Request, Response } from 'express';
import { getRepository } from 'typeorm';
import Orphanage from '../models/Orphanage';
import * as Yup from 'yup';

import orphanageView from '../views/orphanages_views';

export default {
  async index(request: Request, response: Response) {

    const orphanagesRepository = getRepository(Orphanage);

    const orphanages = await orphanagesRepository.find({
      relations: ['images']
    });

    return response.status(200).json(orphanageView.renderMany(orphanages));
  },

  async show(request: Request, response: Response) {

    const { id } = request.params;

    const orphanagesRepository = getRepository(Orphanage);

    const orphanage = await orphanagesRepository.findOneOrFail(id, {
      relations: ['images']
    });

    return response.status(200).json(orphanageView.render(orphanage));  
  },

  async create(request: Request, response: Response) {
    
    const {
      nome,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends
    } = request.body;
  
    const orphanageRepository = getRepository(Orphanage);

    const requestImages = request.files as Express.Multer.File[];

    const images = requestImages.map(image => {
      return { path: image.filename }
    })

    const data = {
      name,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends,
      images
    };

    //esquema para validar os campos
    const schema = Yup.object().shape({
      //se quiser traduzir, só inserir no required()
      name: Yup.string().required('Nome obrigatório'),
      latitude: Yup.number().required(),
      longitude: Yup.number().required(),
      about: Yup.string().required().max(300),
      instructions: Yup.string().required(),
      opening_hours: Yup.string().required(),
      open_on_weekends: Yup.boolean().required(),
      images: Yup.array(
        Yup.object().shape({
          path: Yup.string().required()
        })
      )
    });

    //valida os campos
    await schema.validate(data, {
      //retorna todos os erros de validação que encontrar
      abortEarly: false
    })

    //cria o orfanato
    const orphanage = orphanageRepository.create(data);
  
    //grava no BD
    await orphanageRepository.save(orphanage);
  
    //retorna o obj orphanage e retorna o status code 201 created
    return response.status(201).json(orphanage);
  }
}