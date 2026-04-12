export interface EnderecoResponseDto {
  cep: string;
  logradouro: string;
  complemento: string;
  bairro: string;
  cidade: string;
  estado: string;
  ibge: string;
  gia: string;
  ddd: string;
  siafi: string;
}

export interface ErrorResponseDto {
  mensagem: string;
  statusCode: number;
}

export interface ViaCepResponseDto {
  cep?: string;
  logradouro?: string;
  complemento?: string;
  bairro?: string;
  localidade?: string;
  uf?: string;
  ibge?: string;
  gia?: string;
  ddd?: string;
  siafi?: string;
  erro?: boolean;
}
