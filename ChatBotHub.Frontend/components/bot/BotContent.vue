<template>
  <div class="botContent">
    <header class="botContent__header">
      <div class="logo">RAG AI Bot</div>
      <div class="user">
        <div class="user__name">user</div>
        <div class="user__icon">
          <svg width="20" height="20" viewBox="0 0 20 20">
            <use xlink:href="/sprite.svg#mdi--user"/>
          </svg>
        </div>
      </div>
    </header>
    <div class="botInfo">
      <button
        v-for="(button, index) in botButtons"
        :key="index"
        class="menuIconButton"
        @click="() => openFileList = !openFileList">
        <svg width="24" height="24">
          <use :xlink:href="`/sprite.svg#${button.icon}`"/>
        </svg>
      </button>
      <div class="botInfo__name">Бот для документации</div>
    </div>
    <div class="chatContainer">
      <div
        class="fileSection"
        :class="{ 'closed': !openFileList }">
        <div
          v-for="(file, index) in fileList"
          :key="index"
          class="file">
          <div class="file__icon">
            <svg width="32" height="32" viewBox="0 0 32 32">
              <use xlink:href="/sprite.svg#mdi--file"/>
            </svg>
          </div>
          <div class="file__name">{{ file.name }}</div>
          <button class="file__edit">
            <svg width="20" height="20" viewBox="0 0 20 20">
              <use xlink:href="/sprite.svg#material-symbols--edit-rounded"/>
            </svg>
          </button>
        </div>
      </div>
      <div class="botSection">
        <div
          v-for="(message, index) in chat"
          :key="index"
          class="message">
          <div v-if="message.author === 'user'" class="message__user">
            {{ message.content }}
          </div>
          <div v-if="message.author === 'bot'" class="message__bot">
            <div class="botIcon"></div>
            <div class="content" v-html="renderMarkdown(message.content)"></div>
            <!-- <ContentRenderer
              class="content"
              :value="parseMarkdown(message.content)" /> -->
          </div>
      </div>
      </div>
    </div>
    <footer class="botContent__footer">
      <div class="inputSection">
        <textarea
          class="queryInput"
          placeholder="Введите ваш запрос..."
          rows="3"
          v-model="queryText"
          @keydown.enter.exact.prevent="handleSubmit">
        </textarea>
        <div class="inputSection__buttons">
          <button class="inputSection__button">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <use xlink:href="/sprite.svg#material-symbols--delete-outline-rounded"/>
            </svg>
            Удалить чат
          </button>
          <button class="inputSection__button">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <use xlink:href="/sprite.svg#bi--stars"/>
            </svg>
            Придумать
          </button>  
        </div>
        <button class="submitButton" @click="handleSubmit">
          <svg width="20" height="20" viewBox="0 0 20 20">
            <use xlink:href="/sprite.svg#teenyicons--send-outline"/>
          </svg>
        </button>
      </div>
      <div class="underText">Platform for creating AI bots with RAG</div>
    </footer>
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
  const unsafeHtml = md.render(content);
  return DOMPurify.sanitize(unsafeHtml);
};

const openFileList = ref(true);

const botButtons = [
  {
    icon: 'material-symbols--folder-outline',
  }, 
  {
    icon: 'material-symbols--edit-rounded',
  },
  {
    icon: 'mingcute--code-line',
  },
];

const fileList = [
  {
    name: 'file_1.pdf',
  },
  {
    name: 'file_2.pdf',
  },
];

const handleSubmit = () => {
  if (queryText.value.trim()) {
    console.log('Отправлен запрос:', queryText.value);
    // Здесь будет логика отправки запроса
    queryText.value = '';
  }
};

// const parseMarkdown = computed(() => {
//   return chat.value.map(msg => {
//     if (msg.author === 'bot') {
//       return {
//         _id: `msg-${Date.now()}-${Math.random().toString(36).substring(7)}`,
//         body: parseMarkdown(msg.content)
//       }
//     }
//     return null
//   }).filter(Boolean)
// })

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
.botContent {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100dvh;
  background-color: #2F395B;
  color: #A5B8F1;

  &__header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 6rem;
    padding-inline: 2rem;
    border-bottom: 2px solid #404E7B;
  }

  .logo {
    font-size: 2.4rem;
    font-weight: 900;
    line-height: 2.8rem;
  }

  .user {
    display: flex;
    align-items: center;
    gap: 0.8rem;

    &__name {
      font-size: 1.6rem;
      line-height: 1.8rem;
    }

    &__icon {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 3.6rem;
      width: 3.6rem;
      border-radius: 50%;
      background-color: #4C5B8D;
    }
  }
}

.botInfo {
  margin: 0 auto;
  width: 100%;
  max-width: 86rem;
  display: flex;
  align-items: center;
  height: 6rem;
  padding: 0 0.8rem;
  gap: 0.4rem;

  &__name {
    font-size: 2rem;
    line-height: 2.2rem;
    margin-left: 0.4rem;
  }
}

.chatContainer {
  margin: 0 auto;
  width: 100%;
  max-width: 86rem;
  display: flex;
  padding: 0 2rem;
}

.fileSection {
  padding-right: 2rem;
  padding-top: 1.2rem;
  max-width: 26rem;
  width: 33%;
  opacity: 1;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  transition: all 0.3s ease;

  &.closed {
    width: 0;
    opacity: 0;
  }

  .file {
    position: relative;
    padding: 0 2.8rem 0 1.6rem;
    display: flex;
    gap: 0.8rem;
    align-items: center;
    width: 100%;
    height: 6rem;
    border-radius: 8px;
    background-color: #404E7B;

    &__name {
      width: 6.4rem;
      overflow: hidden;
      font-size: 1.6rem;
      line-height: 2rem;
    }

    &__edit {
      position: absolute;
      top: 0.8rem;
      right: 0.8rem;
      border: none;
      background: none;
      color: #A5B8F1;
    }
  }
}

.menuIconButton {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 3.6rem;
  width: 3.6rem;
  
  background: rgba(255, 255, 255, 0.1);
  color: white;
  border: none;
  border-radius: 8px;

  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background: rgba(255, 255, 255, 0.2);
  }

  &:active {
    transform: scale(0.95);
  }

  svg {
    fill: currentColor;
  }
}

.botContent__footer {
  margin-top: auto;
  width: 100%;
  // padding: 1rem;
  max-width: 86rem;
  // height: 10.6rem;
  margin-inline: auto;
}

.inputSection {
  position: relative;
  
  height: 10.6rem;
  border-radius: 2.4rem;
  color: #A5B8F1;
  background-color: #4B5988;
  padding: 1.2rem;

  &__buttons {
    display: flex;
    gap: 0.8rem;
    padding-block: 0.4rem;
  }

  &__button {
    display: flex;
    align-items: center;
    gap: 0.4rem;
    background: none;
    border: none;
    height: 2.4rem;
    padding: 0 0.6rem;
    border-radius: 1.2rem;
    font-size: 1.2rem;
    color: #A5B8F1;
    border: 0.1rem solid #A5B8F1;
    cursor: pointer;
  }
}

.queryInput {
  color: #A5B8F1;
  background-color: #4B5988;
  background: none;
  width: 100%;
  border: none;
  // border-radius: 0.5rem;
  // height: 10.6rem;
  // border-radius: 2.4rem;
  resize: none;
  font-size: 1.6rem;
  line-height: 2rem;
  height: 5rem;
  transition: all 0.2s;
  // overflow: hidden;
  // box-shadow: 0 1px 2px rgba(0, 0, 0, 0.05);

  &::placeholder {
    color: #A5B8F1;
  }

  &:focus {
    outline: none;
  }
}

.submitButton {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 3.2em;
  width: 3.2rem;
  position: absolute;
  right: 0.8rem;
  bottom: 0.8rem;
  background-color: #A5B8F1;
  border: none;
  color: #6366f1;
  cursor: pointer;
  border-radius: 50%;
  transition: all 0.2s;
}

.submitButton:hover {
  color: #4f46e5;
  transform: translateY(-1px);
}

.submitButton:active {
  transform: translateY(0);
}

.underText {
  height: 3.6rem;
  text-align: center;
  color: #64748b;
  font-size: 1.2rem;
  line-height: 1.6rem;
  font-weight: 200;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #A5B8F1;
}

.botSection {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 2.4rem;
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
  }
}

</style>