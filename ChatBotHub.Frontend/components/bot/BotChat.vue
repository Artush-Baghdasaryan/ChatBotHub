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
    content: `## RAG Architecture

  **Компоненты:**
  1. Retrieval:
    - Векторная база данных
    - Поиск по семантике

  2. Generation:
    - LLM (например, GPT)
    - Контекстный ответ

  \`\`\`python
  # Пример реализации
  def rag_query(question):
    context = retrieve(question)
    return generate(context)
  \`\`\`
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
    display: inline-block;
    float: right;
  }

  &__bot {
    display: flex;
    gap: 1.6rem;
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
  .content :deep(pre) {
  background-color: #1e293b;
  color: #f8fafc;
  padding: 1rem;
  border-radius: 0.5rem;
  overflow-x: auto;
}

  .content :deep(code) {
    font-family: 'Courier New', monospace;
    font-size: 0.9em;
  }

  .content :deep(h1) {
    font-size: 2em;
    margin: 0.67em 0;
  }

  .content :deep(h2) {
    font-size: 1.9rem;
    margin: 0.83em 0;
  }

  .content :deep(ul) {
    padding-left: 2em;
    list-style-type: disc;
  }

  .content :deep(ol) {
    padding-left: 2em;
    list-style-type: decimal;
  }
}
</style>