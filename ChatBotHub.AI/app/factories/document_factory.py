from abc import ABC, abstractmethod
from typing import List, Dict

from llama_index.core import Document

from app.models.create_document_request import CreateDocumentRequest


class DocumentFactory(ABC):
    def __init__(self, request: CreateDocumentRequest):
        self._request = request

    @abstractmethod
    def create_documents(self) -> List[Document]:
        pass

    def _extract_metadata(self) -> Dict:
        return {
            "attachment_id": self._request.attachment_id,
            "file_name": self._request.file_name,
            "file_type": self._request.file_type,
            "description": self._request.description
        }