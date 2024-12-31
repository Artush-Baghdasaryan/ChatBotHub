import chromadb
from llama_index.vector_stores.chroma import ChromaVectorStore


class ChromaService:
    def __init__(self):
        self.__db = chromadb.PersistentClient(path="./chroma_db")
    
    def get_or_create_vector_store(self, collection_name: str):
        collection = self._get_or_create_collection(collection_name)
        return ChromaVectorStore(collection)

    def _get_or_create_collection(self, name: str):
        return self.__db.get_or_create_collection(name)