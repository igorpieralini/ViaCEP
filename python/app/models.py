from pydantic import BaseModel
from typing import Optional


class EnderecoResponseDto(BaseModel):
    cep: str = ""
    logradouro: str = ""
    complemento: str = ""
    bairro: str = ""
    cidade: str = ""
    estado: str = ""
    ibge: str = ""
    gia: str = ""
    ddd: str = ""
    siafi: str = ""


class ErrorResponseDto(BaseModel):
    mensagem: str
    statusCode: int


class ViaCepResponseDto(BaseModel):
    cep: Optional[str] = None
    logradouro: Optional[str] = None
    complemento: Optional[str] = None
    bairro: Optional[str] = None
    localidade: Optional[str] = None
    uf: Optional[str] = None
    ibge: Optional[str] = None
    gia: Optional[str] = None
    ddd: Optional[str] = None
    siafi: Optional[str] = None
    erro: Optional[bool] = None
