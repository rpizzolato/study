import { Request, Response } from 'express';
import { getRepository } from 'typeorm';
import Orphanage from '../models/Orphanage';

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
      name,
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

    //cria o orfanato
    const orphanage = orphanageRepository.create({
      name,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends,
      images
    })
  
    //grava no BD
    await orphanageRepository.save(orphanage);
  
    //retorna o obj orphanage e retorna o status code 201 created
    return response.status(201).json(orphanage);
  }
}