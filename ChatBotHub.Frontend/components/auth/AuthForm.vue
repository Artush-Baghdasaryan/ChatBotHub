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
            :type="input.type"
            class="formInput__input"/>
          <div v-show="true" class="formInput__error">{{ 'error' }}</div>
        </div>
        <button type="submit" :disabled="isSubmitting">
          {{ data_authForm[mode].button }}
        </button>
      </form>
    </div>
  </div>
</template>
<script setup>
import { ref } from 'vue';

const props = defineProps({
  mode: {
    type: String,
    default: 'login',
    validator: (value) => {
      return ['login', 'register'].includes(value);
    },
  },

});

const formData = ref({});
const errors = ref({});
const isSubmitting = ref(false);

const data_authForm = {
  login: {
    label: 'Вход',
    inputs: [
      {
        name: 'Логин',
        type: 'login',
      },
      {
        name: 'Пароль',
        type: 'password',
      },
    ],
    button: 'Войти',
    request: 'api/auth/login',
  },
  register: {
    label: 'Регистрация',
    inputs: [
      {
        name: 'Логин',
        type: 'login',
      },
      {
        name: 'Пароль',
        type: 'password',
      },
    ],
    button: 'Регистрация',
    request: 'api/auth/register',
  },
};

</script>
<style scoped lang="scss">
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
    display: inline-block;
    font-family: 'Roboto', sans-serif;
    color: #A5B8F1;
    font-size: 2rem;
    font-weight: 700;
    line-height: 2.2rem;
    margin-bottom: 2rem;
  }
}

.formInput {
  display: flex;
  flex-direction: column;

  &__name {
    display: inline-block;
    font-family: 'Roboto', sans-serif;
    color: #A5B8F1;
    font-size: 1.2rem;
    font-weight: 200;
    line-height: 1.6rem;
  }

  &__input {
    display: inline-block;
    max-width: 40rem;
  }
}
</style>