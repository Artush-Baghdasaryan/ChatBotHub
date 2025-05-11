import tempfile
from pathlib import Path
from typing import List

from llama_index.core import Document
from llama_index.readers.file import PDFReader

from app.factories.document_factory import DocumentFactory


class PDFDocumentFactory(DocumentFactory):
    def create_documents(self) -> List[Document]:
        with tempfile.NamedTemporaryFile(delete=False, suffix=".pdf") as tmp:
            tmp.write(self._request.file_data)
            tmp_path = tmp.name

        reader = PDFReader()
        docs = reader.load_data(file=Path(tmp_path))

        for doc in docs:
            doc.metadata.update(self._extract_metadata())

        return docs