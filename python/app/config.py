import os


class Config:
    PORT: int = int(os.getenv("PORT", "5003"))
    API_KEY: str = os.getenv("API_KEY", "CHANGE_ME")
    VIACEP_BASE_URL: str = os.getenv("VIACEP_BASE_URL", "https://viacep.com.br/ws")
    VIACEP_TIMEOUT_SECONDS: int = int(os.getenv("VIACEP_TIMEOUT_SECONDS", "10"))
    CACHE_MINUTES: int = int(os.getenv("CACHE_MINUTES", "60"))


config = Config()
