from typing import Annotated, List
from fastapi import Depends
from llama_index.core import StorageContext, VectorStoreIndex, Document
from sympy.matrices.expressions.matadd import factor_of

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
        bot_id: str,
        requests: List[IndexRequest]
    ) -> VectorStoreIndex:
        """Creates or updates a vector index for the given bot_id using the provided requests.
        
        Args:
            bot_id: Bot identifier
            requests: List of index requests to process
            
        Returns:
            VectorStoreIndex: The created or updated index
        """
        
        documents = self.__get_documents(requests)
        print("documents are created", documents)

        if self.__chroma_service.collection_exists(bot_id):
            return self.__update_index(bot_id, documents)

        return self.__index(bot_id, documents)


    @staticmethod
    def __get_documents(requests: List[IndexRequest]) -> List[Document]:
        """Converts index requests into LlamaIndex documents using appropriate factories.
        
        Args:
            requests: List of index requests to convert
            
        Returns:
            List[Document]: Generated documents
        """
        documents = []
        for request in requests:
            factory = DocumentParserProvider.get_parser(request)
            documents_for_req = factory.create_documents()
            documents.extend(documents_for_req)

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