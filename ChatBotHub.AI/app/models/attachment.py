
from pydantic import BaseModel

from app.models.file_model import FileModel


class Attachment(BaseModel):
    file: FileModel
    description: str
