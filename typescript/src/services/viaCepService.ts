import NodeCache from 'node-cache';
import { config } from '../config';
import { buscarCep } from './viaCepClient';
import { EnderecoResponseDto, ViaCepResponseDto } from '../models/dtos';

const cache = new NodeCache({ stdTTL: config.cacheMinutes * 60 });
const CEP_REGEX = /^\d{8}$/;

function mapToDto(data: ViaCepResponseDto): EnderecoResponseDto {
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

export async function consultarCep(cep: string): Promise<EnderecoResponseDto> {
  const cepLimpo = cep.replace(/-/g, '').trim();

  if (!CEP_REGEX.test(cepLimpo)) {
    throw { type: 'validation', message: 'CEP inválido. Informe somente 8 dígitos numéricos.' };
  }

  const cached = cache.get<EnderecoResponseDto>(cepLimpo);
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
