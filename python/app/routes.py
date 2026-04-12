from fastapi import APIRouter
from fastapi.responses import JSONResponse
from app.viacep_service import consultar_cep, CepValidationError, CepNotFoundError
from app.viacep_client import ExternalServiceError
from app.models import EnderecoResponseDto, ErrorResponseDto

router = APIRouter(prefix="/api/cep", tags=["CEP"])


@router.get(
    "/{cep}",
    response_model=EnderecoResponseDto,
    responses={
        400: {"model": ErrorResponseDto, "description": "CEP informado possui formato inválido."},
        404: {"model": ErrorResponseDto, "description": "CEP não encontrado na base da ViaCEP."},
        500: {"model": ErrorResponseDto, "description": "Erro interno ou falha na comunicação com o serviço ViaCEP."},
    },
    summary="Consulta informações de endereço para o CEP informado.",
)
async def get_cep(cep: str):
    try:
        endereco = await consultar_cep(cep)
        return endereco
    except CepValidationError as e:
        return JSONResponse(status_code=400, content={"mensagem": str(e), "statusCode": 400})
    except CepNotFoundError as e:
        return JSONResponse(status_code=404, content={"mensagem": str(e), "statusCode": 404})
    except ExternalServiceError as e:
        return JSONResponse(status_code=500, content={"mensagem": str(e), "statusCode": 500})
    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"mensagem": "Ocorreu um erro interno inesperado.", "statusCode": 500},
        )
