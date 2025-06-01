from typing import Any, List, Optional

from llama_index.core.agent.workflow import FunctionAgent
from llama_index.core.base.llms.types import ChatMessage
from llama_index.core.memory.chat_memory_buffer import ChatMemoryBuffer
from llama_index.core.prompts import RichPromptTemplate
from llama_index.llms.openai import OpenAI

from app.agents.chat_bot.prompts import system_prompt
from app.core.config import OPENAI_MODEL_NAME
from app.models import Message, MessageRoleType
from app.models.chat_bot_model import ChatBotModel


class ChatBotAgent:
    def __init__(
        self,
        chat_bot_model: ChatBotModel,
        tools: List[Any],
        chat_history: List[Message]
    ):
        self.__model = chat_bot_model
        self.__chat_history = chat_history
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
        memory = ChatMemoryBuffer.from_defaults(token_limit=3000)

        chat_history = []

        for chat_message in self.__chat_history:
            role = "user" if chat_message.role == MessageRoleType.USER else "assistant"
            chat_history.append(ChatMessage(role=role, content=chat_message.content))

        print("chat history:", chat_history)
        memory.put_messages(chat_history)
        return memory

    async def query(self, query: str) -> str:
        response = await self.__agent.run(query, memory=self.__memory)
        return response.response.content


