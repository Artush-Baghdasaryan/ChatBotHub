<template>
  <div class="formInput">
    <div class="formInput__name">{{ label }}</div>
    <input
      :value="modelValue"
      :type="type === 'password' && !showPassword ? 'password' : 'text'"
      class="formInput__input"
      :name="name"
      :id="name"
      :placeholder="placeholder"
      @blur="handleBlur"
      @input="handleInput"
    />
    <div class="formInput__error">
      {{ error }}
    </div>
    <div
      v-if="type === 'password'"
      class="formInput__closeEye"
      @click="togglePasswordVisibility">
      <svg width="24" height="24" viewBox="0 0 24 24">
        <use :xlink:href="showPassword ? '/sprite.svg#lucide--eye' : '/sprite.svg#lucide--eye-off'"/>
      </svg>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const props = defineProps({
  modelValue: {
    type: String,
    default: ''
  },
  label: {
    type: String,
    required: true
  },
  name: {
    type: String,
    required: true
  },
  type: {
    type: String,
    default: 'text'
  },
  placeholder: {
    type: String,
    default: ''
  },
  error: {
    type: String,
    default: ''
  },
  validationRules: {
    type: Array,
    default: () => []
  }
})

const emit = defineEmits(['update:modelValue', 'validate'])

const showPassword = ref(false)

const togglePasswordVisibility = () => {
  showPassword.value = !showPassword.value
}

const handleInput = (e) => {
  emit('update:modelValue', e.target.value)
  if (props.validationRules.length) {
    emit('validate', props.name, e.target.value)
  }
}

const handleBlur = () => {
  if (props.validationRules.length) {
    emit('validate', props.name, props.modelValue)
  }
}
</script>

<style scoped>
/* Перенести стили из оригинального компонента */
.formInput {
  display: flex;
  flex-direction: column;
  position: relative;
}

.formInput__name {
  display: inline-block;
  color: #A5B8F1;
  font-size: 1.2rem;
  font-weight: 200;
  line-height: 1.6rem;
  margin-left: 1.2rem;
}

.formInput__input {
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

.formInput__error {
  display: inline-block;
  color: #dd9562;
  font-size: 1.2rem;
  font-weight: 200;
  line-height: 1.6rem;
  margin-left: 1.2rem;
  height: 1.6rem;
}

.formInput__closeEye {
  color: #b7c6ef;
  position: absolute;
  right: 0.8rem;
  top: 2.4rem;
  cursor: pointer;
}
</style>