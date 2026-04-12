import axios, { AxiosInstance } from 'axios';
import axiosRetry from 'axios-retry';
import { config } from '../config';
import { ViaCepResponseDto } from '../models/dtos';

const client: AxiosInstance = axios.create({
  baseURL: config.viaCep.baseUrl,
  timeout: config.viaCep.timeoutMs,
  headers: { Accept: 'application/json' },
});

axiosRetry(client, {
  retries: 3,
  retryDelay: (retryCount) => Math.pow(2, retryCount) * 1000,
  retryCondition: (error) =>
    axiosRetry.isNetworkOrIdempotentRequestError(error) ||
    (error.response !== undefined && error.response.status >= 500),
});

export async function buscarCep(cep: string): Promise<ViaCepResponseDto> {
  try {
    const { data } = await client.get<ViaCepResponseDto>(`/${cep}/json/`);
    return data;
  } catch (error: any) {
    if (error.code === 'ECONNABORTED') {
      throw { type: 'external', message: 'Timeout ao consultar o serviço ViaCEP.' };
    }
    throw { type: 'external', message: 'Falha ao comunicar com o serviço ViaCEP.' };
  }
}
