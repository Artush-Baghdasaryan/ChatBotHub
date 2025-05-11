<template>
  <div class="botSection">
    <div
      v-for="(message, index) in chat"
      :key="index"
      class="message">
      <div v-if="message.author === 'user'" class="message__user">
        {{ message.content }}
      </div>
      <div v-if="message.author === 'bot'" class="message__bot">
        <div class="botIcon">
          <svg width="20" height="20" viewBox="0 0 20 20">
            <use xlink:href="/sprite.svg#fluent--bot-16-filled"/>
          </svg>
        </div>
        <div
          class="content"
          v-html="renderMarkdown(message.content)">
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import MarkdownIt from 'markdown-it';
import DOMPurify from 'dompurify';

const md = new MarkdownIt({
  html: true,
  linkify: true,
  typographer: true
});

const renderMarkdown = (content) => {
  if (process.client) {
    const unsafeHtml = md.render(content);
    return DOMPurify.sanitize(unsafeHtml);
  }
  return md.render(content);
};

const chat = reactive([
  {
    author: 'user',
    content: 'Привет! Можешь кратко объяснить, что такое RAG? Конечно! RAG (Retrieval-Augmented Generation) — это метод, который комбинирует поиск информации (retrieval) и генерацию текста (generation). Он позволяет ИИ сначала находить релевантные данные, а затем использовать их для более точных и информативных ответов. '
  },
  {
  author: 'bot',
  content: `## RAG Architecture (Retrieval-Augmented Generation)

**Основные компоненты системы:**

1. **Retrieval (Поиск):**
   - Векторная база данных (например, Pinecone, Weaviate)
   - Семантический поиск по embeddings
   - Гибридный поиск (семантика + ключевые слова)
   - Фильтрация по метаданным

2. **Generation (Генерация):**
   - Крупная языковая модель (LLM): GPT-4, LLaMA, Claude
   - Контекстуализация запроса
   - Промт-инжиниринг
   - Пост-обработка ответов

**Пример реализации:**

\`\`\`python
def rag_query(question: str, top_k: int = 3) -> str:
    """
    Полноценный пример реализации RAG-пайплайна
    с обработкой длинных строк для тестирования скролла: 
    Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam voluptatum 
    voluptate quod quia quibusdam quas quidem quasi, quos quisquam voluptatem.
    """
    # 1. Получаем релевантные чанки
    embeddings = get_embeddings(question)
    results = vector_db.query(
        query_embeddings=embeddings,
        top_k=top_k,
        filters={"status": "published"}
    )
    
    # 2. Формируем контекст
    context = "\\n".join([doc.text for doc in results])
    
    # 3. Генерируем ответ с помощью LLM
    response = llm.generate(
        prompt=f"Ответь на вопрос на основе контекста:\\n{context}\\n\\nВопрос: {question}",
        temperature=0.7
    )
    
    return response.strip()
\`\`\`

**Дополнительные возможности:**
- 💡 Ранжирование результатов поиска
- 🔍 Рерайтинг запросов
- 📊 Логирование и мониторинг
- ⚙️ Fine-tuning модели под домен

> **Примечание:** Для production-решений рекомендуется добавлять:
> - Кэширование результатов
> - Ограничение скорости запросов
> - Валидацию входных данных
`
},
]);

</script>
<style lang="scss" scoped>
.botSection {
  padding-block: 0.8rem;
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 2.4rem;
  max-height: calc(100dvh - 27rem);
  overflow-y: auto;
  overflow-x: hidden;
  padding-right: 0.8rem;
}

.message {
  width: 100%;

  &__user {
    margin-left: 12rem;
    padding: 1.2rem;
    border-radius: 2rem;
    background-color: #596BA3;
    color: #DAE1F8;
    font-size: 1.6rem;
    line-height: 1.8rem;
    display: block;
    float: right;

    @include tablet {
      margin-left: 11rem;
    }

    @include phone {
      margin-left: 4rem;
    }
  }

  &__bot {
    display: flex;
    gap: 1.6rem;

    @include laptop {
      gap: 0.8rem;
    }
  }

  .botIcon {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 3.2rem;
    min-width: 3.2rem;
    border-radius: 50%;
    background-color: #56669A;
  }

  .content {
    display: inline-block;
    color: #DAE1F8;
    font-size: 1.6rem;
    line-height: 1.8rem;

    h1{
      font-size: 1.6rem;
      line-height: 1.8rem;
    }

    p {
      font-size: 1.6rem;
      line-height: 1.8rem;
    }
  }
}

.message {
  width: 100%;

  .content {
    color: #DAE1F8;
    font-size: 1.6rem;
    line-height: 1.8rem;

    :deep(*) {
      margin-bottom: 1.2rem;
      &:last-child {
        margin-bottom: 0;
      }
    }

    :deep(pre) {
      display: block;
      display: inline-block;
      display: flex;
      background-color: #1e293b;
      color: #f8fafc;
      padding: 1rem;
      border-radius: 0.5rem;
      max-width: 54rem;
      overflow-x: auto;

      @include desktop {
        max-width: calc(100dvw - 64rem);
      }

      @include laptop {
        // flex-basis: 40%;
        max-width: calc(100dvw - 50rem);
      }

      @include tablet {
        max-width: calc(100dvw - 12rem);
      }

      @include phone {
        max-width: calc(100dvw - 8rem);
      }

      &::-webkit-scrollbar-track {
        background-color: #2f395b00;
      }
    }

    :deep(code) {
      font-family: 'Courier New', monospace;
      font-size: 0.9em;
      display: inline-block;
    }

    :deep(h1) {
      font-size: 2em;
      margin: 1.5rem 0 1rem;
      line-height: 1.3;
    }

    :deep(h2) {
      font-size: 1.9rem;
      margin: 1.3rem 0 0.9rem;
      line-height: 1.3;
    }

    :deep(h3) {
      font-size: 1.7rem;
      margin: 1.1rem 0 0.8rem;
    }


    :deep(ul),
    :deep(ol) {
      padding-left: 2em;
      margin: 1rem 0;
      
      li {
        margin-bottom: 0.5rem;
      }
    }

    :deep(ul) {
      list-style-type: disc;
    }

    :deep(ol) {
      list-style-type: decimal;
    }

    :deep(p) {
      margin: 1rem 0;
      line-height: 1.6;
    }

    :deep(hr) {
      margin: 1.5rem 0;
      border: 0;
      height: 1px;
      background-color: rgba(255,255,255,0.1);
    }

    :deep(a) {
      color: #7C9AFF;
      text-decoration: none;
      &:hover {
        text-decoration: underline;
      }
    }

    :deep(img) {
      max-width: 100%;
      height: auto;
      margin: 1rem 0;
      border-radius: 0.3rem;
    }
  }
}
</style>