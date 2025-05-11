from app.factories.dox_document_factory import DocxDocumentFactory
from app.factories.pdf_document_factory import PDFDocumentFactory
from app.factories.text_document_factory import TextDocumentFactory
from app.models.index_request import IndexRequest


class DocumentFactoryProvider:
    @staticmethod
    def get_factory(request: IndexRequest):
        file_type = request.file_type.lower()

        match file_type:
            case "pdf" | "application/pdf":
                return PDFDocumentFactory(request)
            case "txt" | "text/plain":
                return TextDocumentFactory(request)
            case "docx" | "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                return DocxDocumentFactory(request)
            case _:
                raise ValueError(f"No document factory found for type: {file_type}")