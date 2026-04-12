import httpx
from tenacity import retry, stop_after_attempt, wait_exponential, retry_if_exception_type
from app.config import config
from app.models import ViaCepResponseDto


class ExternalServiceError(Exception):
    pass


@retry(
    stop=stop_after_attempt(3),
    wait=wait_exponential(multiplier=1, min=2, max=8),
    retry=retry_if_exception_type((httpx.TransportError, httpx.TimeoutException)),
    reraise=True,
)
async def _fetch_cep(cep: str) -> httpx.Response:
    async with httpx.AsyncClient(
        base_url=config.VIACEP_BASE_URL,
        timeout=config.VIACEP_TIMEOUT_SECONDS,
    ) as client:
        return await client.get(f"/{cep}/json/")


async def buscar_cep(cep: str) -> ViaCepResponseDto:
    try:
        response = await _fetch_cep(cep)
    except httpx.TimeoutException:
        raise ExternalServiceError("Timeout ao consultar o serviço ViaCEP.")
    except httpx.TransportError:
        raise ExternalServiceError("Falha ao comunicar com o serviço ViaCEP.")

    if response.status_code != 200:
        raise ExternalServiceError(f"ViaCEP retornou status inesperado: {response.status_code}")

    return ViaCepResponseDto(**response.json())
