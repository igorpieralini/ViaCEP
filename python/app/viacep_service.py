import re
from cachetools import TTLCache
from app.config import config
from app.models import EnderecoResponseDto, ViaCepResponseDto
from app.viacep_client import buscar_cep, ExternalServiceError

_cache: TTLCache = TTLCache(maxsize=1024, ttl=config.CACHE_MINUTES * 60)
_CEP_REGEX = re.compile(r"^\d{8}$")


def _map_to_dto(data: ViaCepResponseDto) -> EnderecoResponseDto:
    return EnderecoResponseDto(
        cep=data.cep or "",
        logradouro=data.logradouro or "",
        complemento=data.complemento or "",
        bairro=data.bairro or "",
        cidade=data.localidade or "",
        estado=data.uf or "",
        ibge=data.ibge or "",
        gia=data.gia or "",
        ddd=data.ddd or "",
        siafi=data.siafi or "",
    )


class CepValidationError(Exception):
    pass


class CepNotFoundError(Exception):
    pass


async def consultar_cep(cep: str) -> EnderecoResponseDto:
    cep_limpo = cep.replace("-", "").strip()

    if not _CEP_REGEX.match(cep_limpo):
        raise CepValidationError("CEP inválido. Informe somente 8 dígitos numéricos.")

    cached = _cache.get(cep_limpo)
    if cached is not None:
        return cached

    resposta = await buscar_cep(cep_limpo)

    if resposta.erro:
        raise CepNotFoundError(f"CEP '{cep_limpo}' não encontrado.")

    resultado = _map_to_dto(resposta)
    _cache[cep_limpo] = resultado

    return resultado
