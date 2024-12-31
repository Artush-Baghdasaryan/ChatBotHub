import os
from typing import Annotated
from fastapi import Depends, UploadFile
from llama_index.core import StorageContext, VectorStoreIndex, Document

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
        upload_file: UploadFile,
        description: str
    ) -> VectorStoreIndex:
        """Created index for received file"""
        content = upload_file.file.read().decode("utf-8")

        documents = [
            Document(
                text=content,
                metadata={
                    "chat_id": chat_id,
                    "file_name": upload_file.filename,
                    "description": description
                }
            )
        ]

        vector_store = self.__chroma_service.get_or_create_vector_store(chat_id)
        storage_context = StorageContext.from_defaults(
            vector_store=vector_store
        )
        
        return VectorStoreIndex.from_documents(documents, storage_context)

    def get_index(self, chat_id: str) -> VectorStoreIndex:
        """Provides index to query"""
        vector_store = self.__chroma_service.get_or_create_vector_store(chat_id)

        storage_context = StorageContext.from_defaults(
            vector_store=vector_store
        )

        return VectorStoreIndex.from_vector_store(
            vector_store=vector_store,
            storage_context=storage_context
        )
