from typing import Annotated, List
from fastapi import Depends
from llama_index.core import StorageContext, VectorStoreIndex, Document

from app.models.attachment import Attachment
from app.services.chroma_service import ChromaService

class IndexService:
    def __init__(
            self,
            chroma_service: Annotated[ChromaService, Depends(ChromaService)]
    ) -> None:
        self.__chroma_service = chroma_service

    def index(
        self,
        chat_id: str,
        attachments: List[Attachment]
    ) -> VectorStoreIndex:
        """
        Creates an index for a received file. If an index already exists for the given chat_id,
        it will be dropped and recreated.

        Args:
        - chat_id: Unique identifier for the chat session
        - attachment: Attachment object containing the file data and metadata

        Returns:
        A VectorStoreIndex object representing the created index for the document
        """
        documents = [
            Document(
                text=attachment.file.file_data,
                metadata={
                    "chat_id": chat_id,
                    "file_name": attachment.file.file_name,
                    "description": attachment.description
                }
            )
            for attachment in attachments
        ]

        self.__chroma_service.drop_collection_if_exists(chat_id)

        vector_store = self.__chroma_service.get_or_create_vector_store(chat_id)
        print("vector store created", vector_store)
        storage_context = StorageContext.from_defaults(
            vector_store=vector_store
        )
        print("storage context created", storage_context)
        return VectorStoreIndex.from_documents(documents, storage_context)

    def get_index(self, chat_id: str) -> VectorStoreIndex:
        """
        Retrieves an existing index for querying purposes.

        Args:
        - chat_id: Unique identifier for the chat session

        Returns:
        A VectorStoreIndex object for the specified chat_id
        """
        vector_store = self.__chroma_service.get_or_create_vector_store(chat_id)

        storage_context = StorageContext.from_defaults(
            vector_store=vector_store
        )

        return VectorStoreIndex.from_vector_store(
            vector_store=vector_store,
            storage_context=storage_context
        )