<template>
  <transition name="overlay-fade">
    <div 
      v-if="isOpen"
      class="drawer-overlay"
      @click.self="closeDrawer"
    >
      <div class="drawer-container">
        <transition name="drawer-slide">
          <div 
            class="drawer"
            :style="{ width }"
          >
            <div class="drawer__header">
              <h3>{{ title }}</h3>
              <button class="drawer__close" @click="closeDrawer">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                  <path d="M18 6L6 18M6 6L18 18" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                </svg>
              </button>
            </div>
            <div class="drawer__content">
              <slot></slot>
            </div>
            <div class="drawer__footer" v-if="$slots.footer">
              <slot name="footer"></slot>
            </div>
          </div>
        </transition>
      </div>
    </div>
  </transition>
</template>

<script setup>
import { onMounted, onBeforeUnmount } from 'vue';

const props = defineProps({
  isOpen: {
    type: Boolean,
    required: true
  },
  title: {
    type: String,
    default: 'Панель'
  },
  width: {
    type: String,
    default: '320px'
  },
  closeOnOverlayClick: {
    type: Boolean,
    default: true
  }
});

const emit = defineEmits(['close']);

const closeDrawer = () => {
  emit('close');
};

const handleKeydown = (e) => {
  if (e.key === 'Escape' && props.isOpen) {
    closeDrawer();
  }
};

onMounted(() => {
  window.addEventListener('keydown', handleKeydown);
  document.body.style.overflow = 'hidden';
});

onBeforeUnmount(() => {
  window.removeEventListener('keydown', handleKeydown);
  document.body.style.overflow = '';
});
</script>

<style scoped>
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
  background-color: rgba(0, 0, 0, 0.5);
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
  background-color: white;
  box-shadow: 2px 0 20px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  overflow-y: auto;
  will-change: transform;
}

.drawer__header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px;
  border-bottom: 1px solid #f0f0f0;
  position: sticky;
  top: 0;
  background: white;
  z-index: 1;
}

.drawer__close {
  background: none;
  border: none;
  cursor: pointer;
  color: #888;
  padding: 4px;
  margin-left: 12px;
  transition: color 0.2s ease;
}
.drawer__close:hover {
  color: #333;
}
.drawer__close svg {
  display: block;
}

.drawer__content {
  padding: 20px;
  flex-grow: 1;
  overflow-y: auto;
}

.drawer__footer {
  padding: 16px 20px;
  border-top: 1px solid #f0f0f0;
  display: flex;
  justify-content: flex-end;
  gap: 12px;
  position: sticky;
  bottom: 0;
  background: white;
}
</style>