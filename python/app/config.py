import os

from dotenv import load_dotenv

load_dotenv()


class Config:
    PORT: int = int(os.environ["PORT"])
    API_KEY: str = os.environ["API_KEY"]
    VIACEP_BASE_URL: str = os.environ["VIACEP_BASE_URL"]
    VIACEP_TIMEOUT_SECONDS: int = int(os.environ["VIACEP_TIMEOUT_SECONDS"])
    CACHE_MINUTES: int = int(os.environ["CACHE_MINUTES"])


config = Config()
