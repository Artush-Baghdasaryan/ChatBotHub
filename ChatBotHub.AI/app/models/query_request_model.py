from enum import Enum
from typing import List, Optional

from pydantic import BaseModel

from app.models.chat_bot_model import ChatBotModel


class MessageRoleType(Enum):
    USER = 0
    BOT = 1


class Message(BaseModel):
    role: MessageRoleType
    content: str


class QueryRequestModel(BaseModel):
    chat_bot: ChatBotModel
    query: str
    chat_history: List[Message]

