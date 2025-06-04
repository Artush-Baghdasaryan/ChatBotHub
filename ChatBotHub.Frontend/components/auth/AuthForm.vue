<template>
  <div class="authSection">
    <div class="authSection__container">
      <form @submit.prevent="handleSubmit" class="authForm">
        <label class="authForm__label">
          {{ data_authForm[mode].label }}
        </label>
        <BotInput
          v-for="(input, index) in data_authForm[mode].inputs"
          :key="index"
          v-model="formData[input.type]"
          :label="input.name"
          :name="input.type"
          :type="input.type"
          :placeholder="input.name"
          :error="data_formErrors[input.type]"
          :validation-rules="input.validate"
          @validate="validate"
        />
        <BotButton
          buttonType="fulled"
          :title="data_authForm[mode].button"
          class="formInput__button"
          type="submit" />
        <BotButton
          buttonType="link"
          :title="data_authForm[mode].addition.text"
          class="formInput__addition"
          type="button"
          @click="toggleMode" />
      </form>
    </div>
    <BotMessage
      v-model="message.open"
      :text="message.text"
      :duration="2000"
      @close="message.open = false"
    />
  </div>
</template>

<script setup>
import { useValidation } from '@/composables/useValidation'

const { formErrors: data_formErrors, validateField } = useValidation()

const message = reactive({
  open: false,
  text: ''
})

const showMessage = (text) => {
  message.text = text
  message.open = true
}

const isLoading = ref(false)

const mode = ref('login');

const data_authForm = {
  login: {
    label: 'Вход',
    inputs: [
      { name: 'Почта', type: 'email', validate: ['required'], },
      { name: 'Пароль', type: 'password', validate: ['required', 'min:6'], },
    ],
    button: 'Войти',
    endpoint: 'accounts/login',
    addition: { text: 'Регистрация', mode: 'register',
    },
  },
  register: {
    label: 'Регистрация',
    inputs: [
      { name: 'Имя', type: 'name', validate: ['required', 'min:3', 'max:12'], },
      { name: 'Фамилия', type: 'lastName', validate: ['required', 'min:3', 'max:12'], },
      { name: 'Почта', type: 'email', validate: ['required'], },
      { name: 'Пароль', type: 'password', validate: ['required', 'min:6'], },
    ],
    button: 'Регистрация',
    endpoint: 'accounts/register',
    addition: { text: 'Вход', mode: 'login', },
  },
}

// Инициализация formData с реактивностью
const formData = reactive({})

// Функция для инициализации/очистки formData при смене режима
const initFormData = () => {
  data_authForm[mode.value].inputs.forEach(input => formData[input.type] = '')
}

// Инициализация при первом рендере
initFormData()

// Функция для переключения режима
const toggleMode = () => {
  mode.value = data_authForm[mode.value].addition.mode
  initFormData()
}

const validate = (field) => {
  const inputConfig = data_authForm[mode.value].inputs.find(i => i.type === field)
  data_formErrors[field] = validateField(formData[field], inputConfig.validate)
}

const handleSubmit = async () => {
  // Валидация всех полей
  data_authForm[mode.value].inputs.forEach(input => validate(input.type))

  if (Object.values(data_formErrors).some(error => error)) {
    showMessage('Пожалуйста, исправьте ошибки в форме')
    return
  }

  isLoading.value = true
  
  try {
    const payload = data_authForm[mode.value].inputs.reduce((acc, input) => {
      acc[input.type] = formData[input.type].trim()
      return acc
    }, {})

    console.log(payload);

    const response = await fetch(`${import.meta.env.VITE_API_URL}${data_authForm[mode.value].endpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      },
      body: JSON.stringify(payload)
    })

    const data = await response.json()

    if (!response.ok) {
      throw new Error('Ошибка сервера')
    }

    if (mode.value === 'login') {
      // Сохраняем токен и перенаправляем
      // localStorage.setItem('authToken', data.token)
      useCookie('authToken', {
        maxAge: 60 * 60 * 24 * 7, // 1 неделя
        secure: true,
        sameSite: 'strict'
      }).value = data.token
      showMessage('Вход выполнен успешно!')
      navigateTo('/bot');
    } 
    if (mode.value === 'register') {
      // После регистрации переключаем на вход
      mode.value = 'login'
      initFormData()
      showMessage('Регистрация прошла успешно!')
    }
  } catch (error) {
    showMessage('Произошла ошибка при отправке данных')
    console.error('Ошибка:', error)
  } finally {
    isLoading.value = false
  }
}
</script>

<style lang="scss">
.icon {
  width: 24px;
  height: 24px;
  color: currentColor;
}

.authSection {
  background-color: #2F395B;
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100dvh;
  width: 100%;

  &__container {
    max-width: 32rem;
    padding: 2rem;
    background-color: #404E7B;
    display: flex;
    border-radius: 20px;
  }
}

.authForm {
  &__label {
    width: 100%;
    display: inline-block;
    color: #A5B8F1;
    font-size: 2.4rem;
    font-weight: 700;
    line-height: 2.6rem;
    margin-bottom: 2rem;
  }
}

.formInput {
  display: flex;
  flex-direction: column;
  position: relative;

  &__name {
    display: inline-block;
    color: #A5B8F1;
    font-size: 1.2rem;
    font-weight: 200;
    line-height: 1.6rem;
    margin-left: 1.2rem;
  }

  &__input {
    display: inline-block;
    max-width: 40rem;
    color: #A5B8F1;
    font-size: 1.8rem;
    font-weight: 200;
    line-height: 2rem;
    width: 100%;
    height: 40px;
    background: hsl(0 0% 100% / 0);
    border: 2px solid #A5B8F1;
    border-radius: 12px;
    padding: 0 0.8rem;

    &[name="password"] {
      padding: 0 40px 0 8px;
    }

    &::placeholder {
      font-size: 1.6rem;
      font-weight: 200;
      line-height: 1.8rem;
      color: #7d8fc4;
    }

    &:focus-visible {
      border: 2px solid #b7c6ef;
      outline: none;
      
      &::placeholder {
        color: #2f395b00;
      }
    }
  }

  &__error {
    display: inline-block;
    color: #dd9562;
    font-size: 1.2rem;
    font-weight: 200;
    line-height: 1.6rem;
    margin-left: 1.2rem;
    height: 1.6rem;
  }

  &__closeEye {
    color: #b7c6ef;
    position: absolute;
    right: 0.8rem;
    top: 2.4rem;
    cursor: pointer;
  }

  &__button {
    margin-top: 1.6rem;
    width: 100%;
  }

  &__addition {
    margin: 0.8rem auto 0;
  }
}
</style>