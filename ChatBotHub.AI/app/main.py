
from typing import Annotated, List
from fastapi.middleware.cors import CORSMiddleware
from fastapi import Body, Depends, FastAPI, File, Form, UploadFile

from app.core.dependencies import verify_api_key
from app.models.attachment import Attachment
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
    attachments: Annotated[List[Attachment], Body(...)],
    index_service: Annotated[IndexService, Depends(IndexService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> None:
    print("got the endpoint", chat_id)
    index_service.index(chat_id, attachments)
    print("index created")


@app.get("/{chat_id}/query")
def query(
    chat_id: str,
    query: str,
    query_service: Annotated[QueryService, Depends(QueryService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> str:
    return query_service.query(chat_id, query)