from pydantic import BaseModel

class IndexRequest(BaseModel):
    attachment_id: str
    file_name: str
    file_data: bytes
    file_type: str
    description: str