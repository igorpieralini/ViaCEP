export const config = {
  port: parseInt(process.env.PORT!, 10),
  apiKey: process.env.API_KEY!,
  viaCep: {
    baseUrl: process.env.VIACEP_BASE_URL!,
    timeoutMs: parseInt(process.env.VIACEP_TIMEOUT_MS!, 10),
  },
  cacheMinutes: parseInt(process.env.CACHE_MINUTES!, 10),
};
