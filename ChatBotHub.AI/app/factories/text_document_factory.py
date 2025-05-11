from typing import List

from llama_index.core import Document

from app.factories.document_factory import DocumentFactory


class TextDocumentFactory(DocumentFactory):
    def create_documents(self) -> List[Document]:
        text = self._request.file_data.decode("utf-8")

        return [
            Document(
                text=text,
                metadata=self._extract_metadata()
            )
        ]