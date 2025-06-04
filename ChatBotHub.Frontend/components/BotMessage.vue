<template>
  <transition name="bot-message">
    <div 
      class="bot-message"
      v-if="visible"
    >
      <div class="bot-message__content">
        <span class="bot-message__text">{{ text }}</span>
        <button 
          v-if="closable"
          class="bot-message__close"
          @click="closeMessage"
        >
          &times;
        </button>
      </div>
    </div>
  </transition>
</template>

<script setup>
import { ref, watch, onMounted, nextTick } from 'vue'

const props = defineProps({
  text: {
    type: String,
    required: true
  },
  duration: {
    type: Number,
    default: 5000
  },
  closable: {
    type: Boolean,
    default: true
  },
  modelValue: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:modelValue', 'close'])

const visible = ref(false)
let timeoutId = null

const closeMessage = () => {
  visible.value = false
  emit('update:modelValue', false)
  emit('close')
}

const setupAutoClose = () => {
  if (timeoutId) clearTimeout(timeoutId)
  if (props.duration > 0) {
    timeoutId = setTimeout(closeMessage, props.duration)
  }
}

watch(() => props.modelValue, async (newVal) => {
  if (newVal) {
    visible.value = true
    await nextTick()
    setupAutoClose()
  } else {
    visible.value = false
  }
})

watch(() => props.text, () => {
  if (visible.value) {
    setupAutoClose()
  }
})

onMounted(() => {
  visible.value = props.modelValue
  if (visible.value) {
    setupAutoClose()
  }
})
</script>

<style scoped>
.bot-message {
  position: fixed;
  top: 20px;
  left: 50%;
  transform: translateX(-50%);
  padding: 12px 24px;
  border-radius: 8px;
  font-size: 14px;
  background-color: #333;
  color: white;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  z-index: 1000;
  display: flex;
  align-items: center;
  max-width: 80%;
}

.bot-message__content {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.bot-message__text {
  flex-grow: 1;
  margin-right: 16px;
}

.bot-message__close {
  background: none;
  border: none;
  color: white;
  font-size: 20px;
  cursor: pointer;
  padding: 0;
  line-height: 1;
  opacity: 0.7;
  transition: opacity 0.2s;
}

.bot-message__close:hover {
  opacity: 1;
}

/* Анимации */
.bot-message-enter-active,
.bot-message-leave-active {
  transition: all 0.3s ease;
}

.bot-message-enter-from,
.bot-message-leave-to {
  opacity: 0;
  transform: translate(-50%, -20px);
}

.bot-message-enter-to,
.bot-message-leave-from {
  opacity: 1;
  transform: translate(-50%, 0);
}
</style>