<template>
  <transition name="fade">
    <div 
      v-if="isOpen"
      class="modal-overlay"
      @click.self="closeModal" >
      <transition name="scale">
        <div class="modal">
          <div class="modal__header">
            <h3>{{ title }}</h3>
            <BotButton
              buttonType="icon-info"
              icon="material-symbols--close-rounded"
              @click="closeModal" />
          </div>
          <div class="modal__content">
            <slot></slot>
          </div>
          <div class="modal__footer" v-if="$slots.footer">
            <slot name="footer"></slot>
          </div>
        </div>
      </transition>
    </div>
  </transition>
</template>

<script setup>
import BotButton from './BotButton.vue';

const props = defineProps({
  isOpen: {
    type: Boolean,
    required: true,
    default: false,
  },
  title: {
    type: String,
    default: 'Диалоговое окно'
  }
});

const emit = defineEmits(['close']);

const closeModal = () => {
  emit('close');
};
</script>

<style scoped>
/* Анимации */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.scale-enter-active,
.scale-leave-active {
  transition: all 0.3s ease;
}

.scale-enter-from,
.scale-leave-to {
  transform: scale(0.95);
  opacity: 0;
}

/* Основные стили */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  backdrop-filter: blur(2px);
}

.modal {
  background-color: #2F395B;
  color: #A5B8F1;
  border-radius: 8px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
  max-width: 90%;
  width: 500px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal__header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px 20px;
}

.modal__content {
  padding: 20px;
}

.modal__footer {
  padding: 16px 20px;
  border-top: 1px solid #eee;
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
</style>