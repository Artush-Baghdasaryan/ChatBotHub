import tempfile
from pathlib import Path
from typing import List

from llama_index.core import Document
from llama_index.readers.file import DocxReader

from app.document_parsers.document_parser import DocumentParser


class DocxDocumentParser(DocumentParser):
    def create_documents(self) -> List[Document]:
        with tempfile.NamedTemporaryFile(delete=False, suffix=".docx") as tmp:
            tmp.write(self._request.file_data)
            tmp_path = tmp.name

        reader = DocxReader()
        docs = reader.load_data(file=Path(tmp_path))

        for doc in docs:
            metadata = self._extract_metadata()
            doc.metadata.update(metadata)

        return docs