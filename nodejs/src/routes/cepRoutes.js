const { Router } = require('express');
const { consultarCep } = require('../services/viaCepService');

const router = Router();

router.get('/:cep', async (req, res) => {
  const { cep } = req.params;

  try {
    const endereco = await consultarCep(cep);
    return res.json(endereco);
  } catch (error) {
    if (error.type === 'validation') {
      return res.status(400).json({ mensagem: error.message, statusCode: 400 });
    }
    if (error.type === 'notFound') {
      return res.status(404).json({ mensagem: error.message, statusCode: 404 });
    }
    if (error.type === 'external') {
      return res.status(500).json({ mensagem: error.message, statusCode: 500 });
    }
    console.error(error);
    return res.status(500).json({ mensagem: 'Ocorreu um erro interno inesperado.', statusCode: 500 });
  }
});

module.exports = router;
