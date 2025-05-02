<template>
  <div class="authSection">
    <div class="authSection__container">
      <form @submit.prevent="handleSubmit" class="authForm">
        <label class="authForm__label">
          {{ data_authForm[mode].label }}
        </label>
        <div
          v-for="(input, index) in data_authForm[mode].inputs"
          :key="index"
          class="formInput">
          <div class="formInput__name">{{ input.name }}</div>
          <input
            v-model="formData[input.type]"
            :type="input.type === 'password' && !openPassword ? 'password' : 'text'"
            class="formInput__input"
            :name="input.type"
            :id="input.type"
            :placeholder="input.name"
            @blur="validate(input.type)"
            @input="validate(input.type)"/>
          <div class="formInput__error">
            {{ data_formErrors[input.type] }}
          </div>
          <div
            v-if="input.type === 'password'"
            class="formInput__closeEye"
            @click="() => openPassword = !openPassword">
            <svg class="icon" width="24" height="24" viewBox="0 0 24 24">
              <use :xlink:href="openPassword ? '/sprite.svg#lucide--eye' : '/sprite.svg#lucide--eye-off'"/>
            </svg>
          </div>
        </div>
        <button
          class="formInput__button"
          type="submit"
          :disabled="isSubmitting">
          {{ data_authForm[mode].button }}
        </button>
        <button
          class="formInput__addition"
          type="button"
          @click="() => mode = data_authForm[mode].addition.mode">
          {{ data_authForm[mode].addition.text }}
        </button>
      </form>
    </div>
  </div>
</template>
<script setup>
const formData = reactive({});
const isSubmitting = ref(false);

const mode = ref('login');
const openPassword = ref(false);

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
};

data_authForm[mode.value].inputs.forEach(input => {
  formData[input.type] = '';
})

const data_formErrors = reactive(data_authForm[mode.value].inputs.reduce((result, item) => {
  return {
    ...result,
    [item.type]: null,
  }
}, {}));

const getWordEnding = (count) => {
  const lastTwo = count % 100;
  const lastOne = count % 10;
  
  if (lastTwo >= 11 && lastTwo <= 14) return 'ов';
  
  switch (lastOne) {
    case 1: return '';
    case 2:
    case 3:
    case 4: return 'а';
    default: return 'ов';
  }
};

const validate = (field) => {
  const value = formData[field];
  const validateTypes = {
    required: {
      pattern: /^required$/,
      test: () => !value.trim(),
      message: () => 'Обязательное поле!',
    },
    min: {
      pattern: /^min:(\d+)$/,
      test: (match) => value.length < parseInt(match[1]),
      message: (match) => `Минимум ${match[1]} символ${getWordEnding(parseInt(match[1]))}`,
    },
    max: {
      pattern: /^max:(\d+)$/,
      test: (match) => value.length > parseInt(match[1]),
      message: (match) => `Максимум ${getWordEnding(parseInt(match[1]))} символов`,
    },
  };

  const data_validate = data_authForm[mode.value].inputs.find((item) => item.type === field).validate;
  if (!data_validate) return;

  for (const rule of data_validate) { // валидация для полей
    for (const validatorName in validateTypes) { // поиск нужного правила
      const validator = validateTypes[validatorName];
      const match = rule.match(validator.pattern);
      if (match) {
        if (validator.test(match)) {
          data_formErrors[field] = validator.message(match);
          return; // Прекращаем при первой ошибке
        }
        break; // Переходим к следующему правилу
      }
    };
    data_formErrors[field] = null;
  }
};

const handleSubmit = () => {
  if (true) {
    console.log('Form is valid, submitting:', formData);
    // Отправка данных на сервер
  } else {
    console.log('Form has errors');
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
    background-color: #b7c6ef;
    border-radius: 20px;
    border: none;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 40px;
    color: #404E7B;
    font-size: 1.6rem;
    text-transform: none;
    cursor: pointer;
    transition: all 0.1s ease-in-out;

    &:hover {
      background-color: #97ade8;
    }

    &:active {
      transition: all 0.1s ease-in-out;
    }

    &:disabled, &:hover:disabled {
      opacity: 0.5;
      box-shadow: none;
      cursor: default;
    }
  }

  &__addition {
    margin: 0.8rem auto 0;
    color: #b7c6ef;
    background-color: #2f395b00;
    border: none;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    font-size: 1.6rem;
    font-weight: 200;
    line-height: 1.8rem;
    text-transform: none;
    cursor: pointer;
    transition: all 0.1s ease-in-out;
    
    &:hover {
      color: #ccd6f2;
    }
  }
}
</style>