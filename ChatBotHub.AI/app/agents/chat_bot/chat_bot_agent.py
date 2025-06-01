from typing import Any, List, Optional

from llama_index.core.agent.workflow import FunctionAgent
from llama_index.core.memory.chat_memory_buffer import ChatMemoryBuffer
from llama_index.core.prompts import RichPromptTemplate
from llama_index.llms.openai import OpenAI

from app.agents.chat_bot.prompts import system_prompt
from app.core.config import OPENAI_MODEL_NAME
from app.models.chat_bot_model import ChatBotModel


class ChatBotAgent:
    def __init__(
        self,
        chat_bot_model: ChatBotModel,
        tools: List[Any],
        session_id: Optional[str] = None,
    ):
        self.__model = chat_bot_model
        self.__session_id = session_id
        self.__tools = tools

        self.__agent = self.__create_agent()
        self.__memory = self.__create_memory_buffer()


    def __create_agent(self) -> FunctionAgent:
        prompt = self.__get_system_prompt()

        return FunctionAgent(
            name=self.__model.name,
            description=self.__model.description,
            system_prompt=prompt,
            llm=OpenAI(model=OPENAI_MODEL_NAME),
            tools=self.__tools
        )


    def __get_system_prompt(self) -> str:
        prompt_template = RichPromptTemplate(system_prompt)
        return prompt_template.format(
            bot_name=self.__model.name,
            bot_description=self.__model.description
        )


    def __create_memory_buffer(self) -> Optional[ChatMemoryBuffer]:
        if not self.__session_id:
            return None

        return ChatMemoryBuffer.from_defaults(
            chat_store_key=self.__session_id,
            token_limit=3000
        )

    async def query(self, query: str) -> str:
        response = await self.__agent.run(query, memory=self.__memory)
        return response.response.content


