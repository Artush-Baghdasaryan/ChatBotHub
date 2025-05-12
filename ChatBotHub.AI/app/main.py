from typing import Annotated, List
from fastapi.middleware.cors import CORSMiddleware
from fastapi import Body, Depends, FastAPI, File, Form, UploadFile
from dotenv import load_dotenv
import os

from app.core.dependencies import verify_api_key
from app.models.index_request import IndexRequest, IndexRequestModel
from app.models.query_request_model import QueryRequestModel
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

load_dotenv()

@app.post("/{bot_id}/index")
def index(
    bot_id: str,
    index_requests: Annotated[List[IndexRequestModel], Body(...)],
    index_service: Annotated[IndexService, Depends(IndexService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> None:
    """Index documents for a specific bot.
    
    Args:
        bot_id: Unique identifier for the bot
        index_requests: List of documents to be indexed
    """
    print("got the endpoint", bot_id)
    requests = [IndexRequest.map(e) for e in index_requests]
    index_service.index(bot_id, requests)

    print("index created")


@app.get("/{bot_id}/query")
def query(
    bot_id: str,
    query_request: Annotated[QueryRequestModel, Body(...)],
    query_service: Annotated[QueryService, Depends(QueryService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> str:
    print("got in endpoint")
    """Query the indexed documents for a specific bot.
    
    Args:
        bot_id: Unique identifier for the bot
        query_request: Query parameters and text
        
    Returns:
        str: Response to the query
    """
    return query_service.query(bot_id, query_request)