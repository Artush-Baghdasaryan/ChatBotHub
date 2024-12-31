from typing import Annotated

from fastapi import Depends

from app.services.index_service import IndexService


class QueryService:
    def __init__(self, index_service: Annotated[IndexService, Depends(IndexService)]):
        self.__index_service = index_service

    def query(self, chat_id: str, query: str):
        index = self.__index_service.get_index(chat_id)
        return index.as_query_engine().query(query)
