from typing import Annotated

from fastapi import Depends

from app.services.index_service import IndexService


class QueryService:
    def __init__(self, index_service: Annotated[IndexService, Depends(IndexService)]):
        self.__index_service = index_service

    def query(self, chat_id: str, query: str):
        """
        Executes a query against the index associated with the specified chat_id.

        Args:
        - chat_id: Unique identifier for the chat session
        - query: The query string to execute against the index

        Returns:
        The query response from the index's query engine
        """
        index = self.__index_service.get_index(chat_id)
        return index.as_query_engine().query(query)
