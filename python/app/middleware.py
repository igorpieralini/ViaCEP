import hmac
from fastapi import Request
from fastapi.responses import JSONResponse
from starlette.middleware.base import BaseHTTPMiddleware
from app.config import config


class ApiKeyMiddleware(BaseHTTPMiddleware):
    async def dispatch(self, request: Request, call_next):
        path = request.url.path

        if path.startswith("/docs") or path.startswith("/openapi") or path.startswith("/health"):
            return await call_next(request)

        received = request.headers.get("X-Api-Key", "")
        expected = config.API_KEY

        if not hmac.compare_digest(received, expected):
            return JSONResponse(
                status_code=401,
                content={"mensagem": "Token de acesso inválido ou ausente.", "statusCode": 401},
            )

        return await call_next(request)
