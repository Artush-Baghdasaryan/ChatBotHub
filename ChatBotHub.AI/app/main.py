
from typing import Annotated
from fastapi.middleware.cors import CORSMiddleware
from fastapi import Depends, FastAPI, File, Form, UploadFile

from app.core.dependencies import verify_api_key
from app.services.index_service import IndexService
from app.services.query_service import QueryService


app = FastAPI()

#middlewares
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.post("/{chat_id}/index")
def index(
    chat_id: str,
    upload_file: Annotated[UploadFile, File(...)],
    description: Annotated[str, Form(...)],
    index_service: Annotated[IndexService, Depends(IndexService)],
    api_key: Annotated[str, Depends(verify_api_key)]
) -> None:
    index_service.index(chat_id, upload_file, description)


@app.get("/{chat_id}/query")
def query(
    chat_id: str,
    query: str,
    query_service: Annotated[QueryService, Depends(QueryService)],
    api_key: Annotated[str, Depends(verify_api_key)]
) -> str:
    return query_service.query(chat_id, query)