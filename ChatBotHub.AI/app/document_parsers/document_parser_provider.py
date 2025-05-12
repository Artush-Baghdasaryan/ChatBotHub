from app.document_parsers.dox_document_parser import DocxDocumentParser
from app.document_parsers.pdf_document_parser import PDFDocumentParser
from app.document_parsers.text_document_parser import TextDocumentParser
from app.models.index_request import IndexRequest


class DocumentParserProvider:
    @staticmethod
    def get_parser(request: IndexRequest):
        file_type = request.file_type.lower()

        match file_type:
            case "pdf" | "application/pdf":
                return PDFDocumentParser(request)
            case "txt" | "text/plain":
                return TextDocumentParser(request)
            case "docx" | "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                return DocxDocumentParser(request)
            case _:
                raise ValueError(f"No document factory found for type: {file_type}")