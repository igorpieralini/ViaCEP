const crypto = require('crypto');
const config = require('../config');

function apiKeyMiddleware(req, res, next) {
  const received = req.headers['x-api-key'] || '';
  const expected = config.apiKey;

  const receivedHmac = crypto.createHmac('sha256', expected).update(received).digest();
  const expectedHmac = crypto.createHmac('sha256', expected).update(expected).digest();

  const isValid = crypto.timingSafeEqual(receivedHmac, expectedHmac);

  if (!isValid) {
    return res.status(401).json({
      mensagem: 'Token de acesso inválido ou ausente.',
      statusCode: 401,
    });
  }

  next();
}

module.exports = { apiKeyMiddleware };
