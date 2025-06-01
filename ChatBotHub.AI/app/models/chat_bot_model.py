from typing import List

from pydantic import BaseModel

from app.models.attachment_model import AttachmentModel


class ChatBotModel(BaseModel):
    id: str
    name: str
    description: str
    attachments: List[AttachmentModel]