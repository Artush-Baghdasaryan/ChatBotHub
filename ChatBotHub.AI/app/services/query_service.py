from typing import Annotated, List

from fastapi import Depends

from app.services.index_service import IndexService
from app.models import Message, MessageRoleType, QueryRequestModel
from app.prompts.query_prompt import query_system_prompt


class QueryService:
    def __init__(self, index_service: Annotated[IndexService, Depends(IndexService)]):
        self.__index_service = index_service


    def query(self, bot_id: str, query_req: QueryRequestModel):
        """Execute a query against a specific bot's index.
        
        Args:
            bot_id: The unique identifier of the bot
            query_req: The query request containing the question and context
        """
        query_engine = self.__get_query_engine(bot_id, query_req)
        return query_engine.query(query_req.query)
    

    def __get_query_engine(self, bot_id: str, query_req: QueryRequestModel):
        """Get a query engine instance for a specific bot with custom prompt.
        
        Args:
            bot_id: The unique identifier of the bot
            query_req: The query request containing context for prompt building
        """
        index = self.__index_service.get_index(bot_id)
        system_prompt = QueryService.__build_prompt(query_req.context)
        return index.as_query_engine(text_qa_template=system_prompt)


    @staticmethod
    def __build_prompt(messages: List[Message]):
        """Build a prompt template with chat history.
        
        Args:
            messages: List of chat messages to include in the prompt
        """
        chat_history = QueryService.__build_chat_history(messages)
        return query_system_prompt.replace("{{chat_history}}", chat_history)


    @staticmethod
    def __build_chat_history(messages: List[Message]) -> str:
        """Convert a list of messages into a formatted chat history string.
        
        Args:
            messages: List of chat messages to format
            
        Returns:
            A string containing the formatted chat history
        """
        return "\n".join(
            f"{'User' if m.role == MessageRoleType.USER else 'Assistant'}: {m.content}"
            for m in messages
        )
