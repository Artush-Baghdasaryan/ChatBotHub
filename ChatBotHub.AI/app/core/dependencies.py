



from typing import Annotated
from fastapi import Depends, HTTPException, Security
from fastapi.security import APIKeyHeader

from app.core.config import API_KEY

api_key_header = APIKeyHeader(name="API_KEY")


def verify_api_key(
        api_key: Annotated[str, Depends(api_key_header)]
) -> str:
    if api_key != API_KEY:
        raise HTTPException(status_code=401, detail="Invalid API key")
    return api_key
