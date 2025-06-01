from typing import Annotated, List

from fastapi import Depends
from llama_index.core.tools import QueryEngineTool

from app.agents.chat_bot.chat_bot_agent import ChatBotAgent
from app.models.attachment_model import AttachmentModel
from app.services.index_service import IndexService
from app.models import QueryRequestModel


class QueryService:
    def __init__(self, index_service: Annotated[IndexService, Depends(IndexService)]):
        self.__index_service = index_service


    async def query(self, query_req: QueryRequestModel) -> str:
        """Execute a query against a specific bot's index.
        
        Args:
            query_req: The query request containing the question and context
        """
        query_engine_tools = self.__collect_query_engine_tools(query_req.chat_bot.attachments)
        agent = ChatBotAgent(
            chat_bot_model=query_req.chat_bot,
            tools=query_engine_tools,
            chat_history=query_req.chat_history
        )

        return await agent.query(query_req.query)


    def __collect_query_engine_tools(self, attachments: List[AttachmentModel]):
        return [
            self.__get_query_engine_tool(attachment)
            for attachment in attachments
        ]


    def __get_query_engine_tool(self, attachment: AttachmentModel):
        """Get a query engine instance for a specific attachment.
        
        Args:
            attachment: Attachment model
        """
        index = self.__index_service.get_index(attachment.id)
        query_engine = index.as_query_engine()

        return QueryEngineTool.from_defaults(
            query_engine=query_engine,
            description=f"Use this tool to answer questions about: {attachment.description}",
        )


