export const config = {
  port: parseInt(process.env.PORT || '5002', 10),
  apiKey: process.env.API_KEY || 'CHANGE_ME',
  viaCep: {
    baseUrl: process.env.VIACEP_BASE_URL || 'https://viacep.com.br/ws',
    timeoutMs: parseInt(process.env.VIACEP_TIMEOUT_MS || '10000', 10),
  },
  cacheMinutes: parseInt(process.env.CACHE_MINUTES || '60', 10),
};
