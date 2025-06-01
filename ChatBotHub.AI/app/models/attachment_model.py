from pydantic import BaseModel


class AttachmentModel(BaseModel):
    id: str
    description: str