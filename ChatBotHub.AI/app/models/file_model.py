from datetime import datetime
from pydantic import BaseModel

class FileModel(BaseModel):
    file_name: str
    file_type: str
    file_data: str
    upload_date: datetime
