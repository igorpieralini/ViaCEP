const NodeCache = require('node-cache');
const config = require('../config');
const { buscarCep } = require('./viaCepClient');

const cache = new NodeCache({ stdTTL: config.cacheMinutes * 60 });
const CEP_REGEX = /^\d{8}$/;

function mapToDto(data) {
  return {
    cep: data.cep || '',
    logradouro: data.logradouro || '',
    complemento: data.complemento || '',
    bairro: data.bairro || '',
    cidade: data.localidade || '',
    estado: data.uf || '',
    ibge: data.ibge || '',
    gia: data.gia || '',
    ddd: data.ddd || '',
    siafi: data.siafi || '',
  };
}

async function consultarCep(cep) {
  const cepLimpo = cep.replace(/-/g, '').trim();

  if (!CEP_REGEX.test(cepLimpo)) {
    throw { type: 'validation', message: 'CEP inválido. Informe somente 8 dígitos numéricos.' };
  }

  const cached = cache.get(cepLimpo);
  if (cached) {
    return cached;
  }

  const resposta = await buscarCep(cepLimpo);

  if (!resposta || resposta.erro) {
    throw { type: 'notFound', message: `CEP '${cepLimpo}' não encontrado.` };
  }

  const resultado = mapToDto(resposta);
  cache.set(cepLimpo, resultado);

  return resultado;
}

module.exports = { consultarCep };
