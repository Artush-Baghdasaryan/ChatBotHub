from enum import Enum
from typing import List

from pydantic import BaseModel

class MessageRoleType(Enum):
    USER = 0
    BOT = 1


class Message(BaseModel):
    role: MessageRoleType
    content: str


class QueryRequestModel(BaseModel):
    query: str
    context: List[Message]


