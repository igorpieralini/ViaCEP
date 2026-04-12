import { Request, Response, NextFunction } from 'express';
import crypto from 'crypto';
import { config } from '../config';

export function apiKeyMiddleware(req: Request, res: Response, next: NextFunction): void {
  const received = (req.headers['x-api-key'] as string) || '';
  const expected = config.apiKey;

  const receivedHmac = crypto.createHmac('sha256', expected).update(received).digest();
  const expectedHmac = crypto.createHmac('sha256', expected).update(expected).digest();

  const isValid = crypto.timingSafeEqual(receivedHmac, expectedHmac);

  if (!isValid) {
    res.status(401).json({ mensagem: 'Token de acesso inválido ou ausente.', statusCode: 401 });
    return;
  }

  next();
}
