const axios = require('axios');
const axiosRetry = require('axios-retry').default;
const config = require('../config');

const client = axios.create({
  baseURL: config.viaCep.baseUrl,
  timeout: config.viaCep.timeoutMs,
  headers: { Accept: 'application/json' },
});

axiosRetry(client, {
  retries: 3,
  retryDelay: (retryCount) => Math.pow(2, retryCount) * 1000,
  retryCondition: (error) =>
    axiosRetry.isNetworkOrIdempotentRequestError(error) ||
    (error.response && error.response.status >= 500),
});

async function buscarCep(cep) {
  try {
    const { data } = await client.get(`/${cep}/json/`);
    return data;
  } catch (error) {
    if (error.code === 'ECONNABORTED') {
      throw { type: 'external', message: 'Timeout ao consultar o serviço ViaCEP.' };
    }
    throw { type: 'external', message: 'Falha ao comunicar com o serviço ViaCEP.' };
  }
}

module.exports = { buscarCep };
