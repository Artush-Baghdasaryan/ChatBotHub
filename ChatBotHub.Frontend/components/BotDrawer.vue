<template>
  <transition name="overlay-fade">
    <div 
      v-if="isOpen"
      class="drawer-overlay"
      @click.self="closeDrawer"
    >
      <div class="drawer-container">
        <transition name="drawer-slide">
          <aside 
            class="drawer"
            :style="{ width }">
            <div class="sidebarHeader">
              <BotButton
                buttonType="icon"
                icon="material-symbols--menu-rounded"
                @click="closeDrawer" />
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
              </div>
            </nav>
            <div class="sidebarFooter">
              <BotButton
                buttonType="icon-full"
                title="Настройки"
                icon="material-symbols--settings"
                @click="openModal" />
              <BotButton
                buttonType="icon-full"
                title="Частые вопросы"
                icon="fe--question"
                @click="openModal" />
            </div>
          </aside>
        </transition>
      </div>
      <BotDialog
        :isOpen="isModalOpen" 
        title="Добавить нового бота"
        @close="closeModal">
        <form @submit.prevent="handleSubmit" class="dialogForm">
          <BotInput
            v-model="formData.title"
            label="Название бота"
            name="title"
            type="text"
            placeholder="Введите название"
            :error="formErrors.title"
            :validation-rules="validationRules.title"
            @validate="validate"
          />
          
          <div class="dialogActions">
            <BotButton
              buttonType="outlined"
              title="Отмена"
              @click="closeModal" />
            <BotButton
              buttonType="fulled"
              title="Создать бота"
              type="submit"
              :disabled="isSubmitting" />
          </div>
        </form>
      </BotDialog>
    </div>
  </transition>
</template>

<script setup>
import { ref, reactive, onMounted, onBeforeUnmount } from 'vue'
import { useValidation } from '@/composables/useValidation'

const { formErrors, validateField } = useValidation()

const props = defineProps({
  isOpen: {
    type: Boolean,
    required: true
  },
  width: {
    type: String,
    default: '260px'
  },
  closeOnOverlayClick: {
    type: Boolean,
    default: true
  }
})

const emit = defineEmits(['close'])

// Состояния интерфейса
const isModalOpen = ref(false)
const isSubmitting = ref(false)

// Данные формы
const formData = reactive({
  title: ''
})

// Правила валидации
const validationRules = {
  title: ['required', 'min:3', 'max:50']
}

// Список ботов
const botList = ref([
  {
    id: 1,
    title: 'Бот для документации',
    date: '19.03.2025',
    files: 2,
  },
  {
    id: 2,
    title: 'Бот для tg канала',
    date: '25.02.2025',
    files: 1,
  },
  {
    id: 3,
    title: 'Бот: помощь по доставке',
    date: '18.02.2025',
    files: 4,
  },
  {
    id: 4,
    title: 'Бот для интернет-магазина',
    date: '06.04.2025',
    files: 2,
  }
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

// Управление drawer
const closeDrawer = () => {
  emit('close')
}

const handleKeydown = (e) => {
  if (e.key === 'Escape' && props.isOpen) {
    closeDrawer()
  }
}

// Хуки жизненного цикла
onMounted(() => {
  window.addEventListener('keydown', handleKeydown)
  document.body.style.overflow = 'hidden'
})

onBeforeUnmount(() => {
  window.removeEventListener('keydown', handleKeydown)
  document.body.style.overflow = ''
})
</script>

<style lang="scss" scoped>
/* Плавная анимация оверлея */
.overlay-fade-enter-active,
.overlay-fade-leave-active {
  transition: opacity 0.4s cubic-bezier(0.16, 1, 0.3, 1);
}
.overlay-fade-enter-from,
.overlay-fade-leave-to {
  opacity: 0;
}

/* Плавная анимация выезжания */
.drawer-slide-enter-active,
.drawer-slide-leave-active {
  transition: transform 0.4s cubic-bezier(0.16, 1, 0.3, 1);
}
.drawer-slide-enter-from {
  transform: translateX(-100%);
}
.drawer-slide-leave-to {
  transform: translateX(calc(-100% - 10px));
}

/* Основные стили */
.drawer-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.35);
  z-index: 1000;
  backdrop-filter: blur(3px);
}

.drawer-container {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  overflow: hidden;
}

.drawer {
  position: relative;
  height: 100vh;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
  will-change: transform;
  width: 22rem;
  min-width: 22rem;
  padding: 1.2rem 1.2rem 1.2rem 2rem;
  background-color: #1F2A4F;
  color: white;
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
  }

  &:active {
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