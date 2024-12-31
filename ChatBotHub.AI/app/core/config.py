from starlette.config import Config

config = Config(".env")

#OPENAI
OPENAI_API_KEY = config.get("OPENAI_API_KEY")
OPENAI_MODEL_NAME = config.get("OPENAI_MODEL_NAME")

#API
API_KEY = config.get("API_KEY")