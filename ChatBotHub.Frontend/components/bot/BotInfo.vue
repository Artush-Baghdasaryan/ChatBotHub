<template>
  <div class="botInfo">
    <div class="buttons">
      <BotButton
        v-for="(button, index) in botButtons"
        :key="index"
        buttonType="icon"
        :icon="button.icon"
        @click="button.click" />
    </div>
    <div class="botInfo__name">{{ title }}</div>
  </div>
  <BotDialog
    :isOpen="isModalOpen" 
    :title="modalTitle[modalType]"
    @close="closeModal">
    
  </BotDialog>
</template>
<script setup>
import useWindowSize from '@/composables/useWindowSize'

const { width } = useWindowSize()

const props = defineProps({
  title: {
    type: String,
    default: 'Бот для документации',
  },
});
const emit = defineEmits(['updateFileList']);

const isModalOpen = ref(false);
const modalType = ref(null);

const modalTitle = ref({
  edit: 'Редактирование',
  code: 'Как использовать',
});

const openModal = (type) => {
  modalType.value = type; 
  isModalOpen.value = true;
};

const closeModal = () => {
  modalType.value = null;
  isModalOpen.value = false;
};

const botButtons = [
  {
    icon: 'material-symbols--folder-outline',
    click: () => {
      if (!width.value.isTablet) emit('updateFileList');
      else openModal('file');
    },
  }, 
  {
    icon: 'material-symbols--edit-rounded',
    click: () => openModal('edit'),
  },
  {
    icon: 'mingcute--code-line',
    click: () => openModal('code'),
  },
];

</script>
<style lang="scss" scoped>
.botInfo {
  margin: 0 auto;
  width: 100%;
  max-width: 86rem;
  display: flex;
  // flex-wrap: wrap;
  align-items: center;
  min-height: 6rem;
  padding: 0 0.8rem;
  gap: 0.4rem;
  box-shadow: 
    0 2px 6px 0px #2F395B,
    0 8px 8px -8px #2F395B,
    0 4px 6px -4px rgba(47, 57, 91, 0.4);
  z-index: 1;

  .buttons {
    min-height: 6rem;
    display: flex;
    align-items: center;
    gap: 0.4rem;
  }

  &__name {
    padding-top: 0.8rem;
    font-size: 2rem;
    line-height: 2.4rem;
    margin-left: 0.4rem;
    white-space: nowrap;
    width: 100%;
    overflow: hidden;
  }

  @include phone {
    flex-direction: column-reverse;
    gap: 0;
    align-items: start;
    min-height: 9.2rem;
  }
}
</style>