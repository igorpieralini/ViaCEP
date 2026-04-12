from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from fastapi.responses import JSONResponse
from app.config import config
from app.middleware import ApiKeyMiddleware
from app.routes import router

app = FastAPI(
    title="ViaCEP API - Python",
    version="1.0.0",
    description="API para consulta de endereços via serviço ViaCEP.",
    docs_url="/docs",
    openapi_url="/openapi.json",
)

app.add_middleware(CORSMiddleware, allow_origins=["*"], allow_methods=["*"], allow_headers=["*"])
app.add_middleware(ApiKeyMiddleware)


@app.get("/health", tags=["Health"])
async def health():
    return JSONResponse(content={"status": "Healthy"})


app.include_router(router)

if __name__ == "__main__":
    import uvicorn

    uvicorn.run(app, host="0.0.0.0", port=config.PORT)
