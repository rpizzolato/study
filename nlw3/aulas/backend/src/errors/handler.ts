import { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (error, request, response, next) => {
  //nos mostra o erro real
  console.error(error);
  
  //retorna ao user uma msg de error genérica
  return response.status(500).json({ message: 'Internal Server Error' });
}

export default errorHandler;