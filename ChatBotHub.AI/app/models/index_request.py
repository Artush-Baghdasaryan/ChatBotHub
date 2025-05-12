from pydantic import BaseModel

class IndexRequestModel(BaseModel):
    attachment_id: str
    file_name: str
    file_data: str
    file_type: str
    description: str

class IndexRequest(BaseModel):
    attachment_id: str
    file_name: str
    file_data: bytes
    file_type: str
    description: str

    @staticmethod
    def map(model: IndexRequestModel):
        return IndexRequest(
            attachment_id=model.attachment_id,
            file_name=model.file_name,
            file_data=model.file_data.encode("utf-8"),
            file_type=model.file_type,
            description=model.description
        )