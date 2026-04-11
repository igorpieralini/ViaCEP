const config = {
  port: parseInt(process.env.PORT, 10) || 5001,
  apiKey: process.env.API_KEY || 'CHANGE_ME',
  viaCep: {
    baseUrl: process.env.VIACEP_BASE_URL || 'https://viacep.com.br/ws',
    timeoutMs: parseInt(process.env.VIACEP_TIMEOUT_MS, 10) || 10000,
  },
  cacheMinutes: parseInt(process.env.CACHE_MINUTES, 10) || 60,
};

module.exports = config;
