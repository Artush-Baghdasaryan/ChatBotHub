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
          @click="() => mode = data_authForm[mode].addition.mode" />
      </form>
    </div>
  </div>
</template>
<script setup>
import { useValidation } from '@/composables/useValidation'

const { formErrors: data_formErrors, validateField } = useValidation()

const formData = reactive({})
const mode = ref('login')

const data_authForm = {
  login: {
    label: 'Вход',
    inputs: [
      {
        name: 'Логин',
        type: 'login',
        validate: ['required', 'min:3', 'max:12'],
      },
      {
        name: 'Пароль',
        type: 'password',
        validate: ['required', 'min:6'],
      },
    ],
    button: 'Войти',
    request: 'api/auth/login',
    addition: {
      text: 'Регистрация',
      mode: 'register',
    },
  },
  register: {
    label: 'Регистрация',
    inputs: [
      {
        name: 'Логин',
        type: 'login',
        validate: ['required', 'min:3', 'max:12'],
      },
      {
        name: 'Пароль',
        type: 'password',
        validate: ['required', 'min:6'],
      },
    ],
    button: 'Регистрация',
    request: 'api/auth/register',
    addition: {
      text: 'Вход',
      mode: 'login',
    },
  },
}

// Инициализация formData
data_authForm[mode.value].inputs.forEach(input => {
  formData[input.type] = ''
})

const validate = (field) => {
  const inputConfig = data_authForm[mode.value].inputs.find(i => i.type === field)
  data_formErrors[field] = validateField(formData[field], inputConfig.validate)
}

const handleSubmit = async () => {
  // 1. Валидация всех полей
  data_authForm[mode.value].inputs.forEach(input => {
    validate(input.type, formData[input.type]);
  })

  // 2. Проверка ошибок валидации
  if (Object.values(data_formErrors).some(Boolean)) {
    console.error('Validation errors:', data_formErrors)
    return
  }

  // 3. Подготовка данных для отправки
  const payload = data_authForm[mode.value].inputs.reduce((acc, input) => {
    acc[input.type] = formData[input.type].trim()
    return acc
  }, {})

  // 4. Отправка на сервер
  // try {
  //   const response = await fetch(data_authForm[mode.value].request, {
  //     method: 'POST',
  //     headers: {
  //       'Content-Type': 'application/json',
  //       'Accept': 'application/json'
  //     },
  //     credentials: 'include', // если нужны куки
  //     body: JSON.stringify(payload)
  //   })

  //   const data = await response.json()

  //   if (!response.ok) {
  //     // Обработка ошибок от сервера
  //     if (data.errors) {
  //       Object.entries(data.errors).forEach(([field, error]) => {
  //         data_formErrors[field] = error
  //       })
  //     }
  //     throw new Error(data.message || 'Failed to submit form')
  //   }

  //   // Успешная отправка
  //   console.log('Success:', data)
    
  //   // Редирект или другие действия
  //   if (mode.value === 'login') {
  //     // Перенаправление после входа
  //   } else {
  //     // Действия после регистрации
  //     mode.value = 'login' // переключить на форму входа
  //   }

  // } catch (error) {
  //   console.error('Submission error:', error)
  //   // Можно добавить отображение ошибки пользователю
  // }
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