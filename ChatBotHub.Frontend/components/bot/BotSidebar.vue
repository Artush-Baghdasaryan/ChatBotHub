<template>
  <aside
    class="botSidebar"
    :class="{ 'collapsed': !isMenuOpen }">
    <div class="sidebarHeader">
      <BotButton
        buttonType="icon"
        icon="material-symbols--menu-rounded"
        @click="toggleMenu" />
      <BotButton
        buttonType="icon"
        icon="material-symbols--edit-rounded"
        @click="openModal" />
    </div>
    <nav class="sidebarList">
      <div
        v-for="item in botList" 
        :key="item.id"
        class="sidebarBot">
        <div class="sidebarBot__title">{{ item.title }}</div>
        <div class="sidebarBot__info">
          <div class="sidebarBot__date">{{ item.date }}</div>
          <div class="sidebarBot__files">
            <div class="sidebarBot__count">{{ item.files }}</div>
            <svg width="16" height="16" viewBox="0 0 16 16">
              <use xlink:href="/sprite.svg#mdi--file"/>
            </svg>
          </div>
        </div>
        <!-- <BotButton
          buttonType="icon-info"
          icon="charm--menu-meatball"
          class="sidebarBot__menu" 
          @click="() => 1"/> -->
      </div>
    </nav>
    <div class="sidebarFooter">
      <BotButton
        :buttonType="!isMenuOpen ? 'icon' : 'icon-full'"
        title="Настройки"
        icon="material-symbols--settings"
        @click="openModal" />
      <BotButton
        :buttonType="!isMenuOpen ? 'icon' : 'icon-full'"
        title="Частые вопросы"
        icon="fe--question"
        @click="openModal" />
    </div>
    <BotDialog
      :isOpen="isModalOpen" 
      title="Добавить нового бота"
      @close="closeModal">
      <form @submit.prevent="handleSubmit" class="dialogForm">
        <BotInput
          v-model="formData.title"
          label="Название"
          name="title"
          type="text"
          placeholder="Введите название"
          :error="formErrors.title"
          :validation-rules="validationRules.title"
          @validate="validate"
        />
        <BotInput
          v-model="formData.description"
          label="Описание"
          name="description"
          type="text"
          placeholder="Введите описание"
          :error="formErrors.description"
          :validation-rules="validationRules.description"
          @validate="validate"
        />
        <div class="dialogActions">
          <BotButton
            buttonType="fulled"
            title="Создать бота"
            type="submit"
            :disabled="isSubmitting" />
          <BotButton
            buttonType="outlined"
            title="Отмена"
            @click="closeModal" />
        </div>
      </form>
    </BotDialog>
  </aside>
</template>

<script setup>
import { useValidation } from '@/composables/useValidation'

const { formErrors, validateField, validateForm } = useValidation()

// Состояния интерфейса
const isMenuOpen = ref(true)
const isModalOpen = ref(false)
const isSubmitting = ref(false)

// Данные формы
const formData = reactive({
  title: '',
  description: '',
})

// Правила валидации
const validationRules = {
  title: ['required', 'min:3', 'max:50'],
  description: ['required', 'min:3', 'max:50'],
}

// Список ботов
const botList = ref([
  {
    id: 1,
    title: 'Бот для документации',
    date: '19.03.2025',
    files: 2,
  },
  // ... остальные боты
])

// Валидация поля
const validate = (fieldName) => {
  formErrors[fieldName] = validateField(formData[fieldName], validationRules[fieldName])
}

// Отправка формы
const handleSubmit = async () => {
  // Валидируем все поля
  Object.keys(formData).forEach(field => validate(field))
  
  // Проверяем ошибки
  if (Object.values(formErrors).some(Boolean)) {
    console.error('Form has validation errors', formErrors)
    return
  }

  isSubmitting.value = true
  
  try {
    console.log('Creating bot:', formData)
    
    // Имитация запроса
    await new Promise(resolve => setTimeout(resolve, 1000))
    
    // Добавляем нового бота
    botList.value.unshift({
      id: Date.now(),
      title: formData.title,
      date: new Date().toLocaleDateString('ru-RU'),
      files: 0
    })
    
    closeModal()
    
  } catch (error) {
    console.error('Error creating bot:', error)
  } finally {
    isSubmitting.value = false
  }
}

// Сброс формы
const resetForm = () => {
  formData.title = ''
  Object.keys(formErrors).forEach(key => formErrors[key] = '')
}

// Управление модальным окном
const openModal = () => {
  isModalOpen.value = true
}

const closeModal = () => {
  isModalOpen.value = false
  resetForm()
}

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value
}
</script>

<style lang="scss" scoped>
.botSidebar {
  width: 26rem;
  min-width: 26rem;
  padding: 1.2rem 1.2rem 1.2rem 2rem;
  background-color: #1F2A4F;
  color: white;
  position: relative;
  transition: all 0.3s ease;
  display: flex;
  flex-direction: column;

  &.collapsed {
    width: 6rem;
    min-width: 6rem;
    padding: 1.2rem;

    .sidebarList {
      display: none;
    }

    .sidebarHeader {
      gap: 1.2rem;
      flex-direction: column;
    }
  }
}

.sidebarHeader {
  display: flex;
  justify-content: space-between;
  padding-bottom: 1.2rem;
}

.sidebarList {
  margin-top: 6rem;
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  max-height: 40rem;
  height: 60rem;
  width: 100%;
  max-height: calc(100dvh - 40.6rem);
  overflow-y: auto;

  &::-webkit-scrollbar-track {
    background-color: #2f395b00;
  }
}

.sidebarBot {
  width: 100%;
  height: 6rem;
  padding: 0.8rem;
  border-radius: 8px;
  color: #A5B8F1;
  background-color: #2C3963;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  position: relative;
  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background-color: #404E7B;

    .sidebarBot__menu {
      display: inline-block;
    }
  }

  &:active{
    background-color: #495b96;
  }

  &__title {
    height: 2rem;
    font-size: 1.6rem;
    line-height: 2rem;
    color: #A5B8F1;
    margin-right: 2rem;
    white-space: nowrap;
    overflow: hidden;
  }

  &__info {
    display: flex;
    justify-content: space-between;
  }

  &__date {
    font-size: 1.2rem;
    line-height: 1.6rem;
  }

  &__files {
    display: flex;
    flex-direction: row;
    gap: 0.2rem;
  }

  &__count {
    font-size: 1.2rem;
    line-height: 1.6rem;
  }
  
  &__menu {
    display: none;
    position: absolute;
    top: 0.2rem;
    right: 0.6rem;
    z-index: 2;
  }
  
  // &__menuList {
  //   position: absolute;
  //   top: 1.6rem;
  //   right: -6rem;
  //   padding: 0.8rem;
  //   border-radius: 8px;
  //   background-color: #2d3e6f;
  //   list-style-type: none;
  //   z-index: 20;
  // }
}

.sidebarFooter {
  margin-top: auto;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.dialogActions {
  margin-top: 0.8rem;
  display: flex;
  justify-content: space-between;
}
</style>