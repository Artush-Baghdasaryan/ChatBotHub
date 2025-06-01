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

@app.post("/index")
def index(
    index_requests: Annotated[List[IndexRequestModel], Body(...)],
    index_service: Annotated[IndexService, Depends(IndexService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> None:
    """Index documents for a specific bot.
    
    Args:
        index_requests: List of documents to be indexed
    """
    print("got the endpoint")
    requests = [IndexRequest.map(e) for e in index_requests]
    index_service.index(requests)

    print("index created")


@app.post("/query")
async def query(
    query_request: Annotated[QueryRequestModel, Body(...)],
    query_service: Annotated[QueryService, Depends(QueryService)],
    #api_key: Annotated[str, Depends(verify_api_key)]
) -> str:
    """Query the indexed documents for a specific bot.
    
    Args:
        query_request: Query parameters and text
        
    Returns:
        str: Response to the query
    """
    print("got in endpoint")
    return await query_service.query(query_request)