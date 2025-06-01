from typing import Annotated, List
from fastapi import Depends
from llama_index.core import StorageContext, VectorStoreIndex, Document

from app.document_parsers.document_parser_provider import DocumentParserProvider
from app.models import IndexRequest
from app.services import ChromaService

class IndexService:
    def __init__(
            self,
            chroma_service: Annotated[ChromaService, Depends(ChromaService)]
    ) -> None:
        self.__chroma_service = chroma_service


    def index(
        self,
        requests: List[IndexRequest]
    ) -> None:
        """Creates or updates a vector index for the given bot_id using the provided requests.
        
        Args:
            requests: List of index requests to process
        """

        for request in requests:
            documents = self.__get_documents(request)
            print("documents are created", documents)

            if self.__chroma_service.collection_exists(request.attachment_id):
                self.__update_index(request.attachment_id, documents)

            self.__index(request.attachment_id, documents)


    @staticmethod
    def __get_documents(request: IndexRequest) -> List[Document]:
        """Converts index requests into LlamaIndex documents using appropriate factories.

        Args:
            request: Request with attachment data

        Returns:
            List[Document]: Generated documents
        """

        factory = DocumentParserProvider.get_parser(request)
        documents = factory.create_documents()
        return documents


    def __index(self, collection_name: str, documents: List[Document]):
        """Creates a new vector index for the given collection.
        
        Args:
            collection_name: Name of the collection
            documents: Documents to index
            
        Returns:
            VectorStoreIndex: The created index
        """
        self.__chroma_service.drop_collection_if_exists(collection_name)

        vector_store = self.__chroma_service.get_or_create_vector_store(collection_name)
        storage_context = StorageContext.from_defaults(vector_store=vector_store)

        return VectorStoreIndex.from_documents(documents, storage_context=storage_context)


    def __update_index(self, collection_name: str, documents: List[Document]):
        """Updates an existing vector index with new documents.
        
        Args:
            collection_name: Name of the collection
            documents: New documents to add
            
        Returns:
            VectorStoreIndex: The updated index
        """
        vector_store = self.__chroma_service.get_or_create_vector_store(collection_name)

        storage_context = StorageContext.from_defaults(vector_store=vector_store)
        index = VectorStoreIndex.from_vector_store(
            vector_store=vector_store,
            storage_context=storage_context
        )

        for document in documents:
            index.insert(document)

        return index


    def get_index(self, chat_id: str) -> VectorStoreIndex:
        """Retrieves an existing vector index for querying.
        
        Args:
            chat_id: Chat session identifier
            
        Returns:
            VectorStoreIndex: The retrieved index
        """
        vector_store = self.__chroma_service.get_or_create_vector_store(chat_id)

        storage_context = StorageContext.from_defaults(
            vector_store=vector_store
        )

        return VectorStoreIndex.from_vector_store(
            vector_store=vector_store,
            storage_context=storage_context
        )