<template>
  <div
    class="fileSection"
    :class="{ 'closed': !openSection }">
    <div class="fileList">
      <div
        v-for="(file, index) in fileList"
        :key="index"
        class="file"
        @click="openModal">
        <div class="file__icon">
          <svg width="32" height="32" viewBox="0 0 32 32">
            <use xlink:href="/sprite.svg#mdi--file"/>
          </svg>
        </div>
        <div class="file__name">{{ file.name }}</div>
        <BotButton
          style="display: none;"
          class="editIcon"
          buttonType="icon-info"
          icon="material-symbols--edit-rounded" />
      </div>
    </div>
    <BotButton
      buttonType="icon-full-center"
      title="Добавить"
      icon="ic--round-plus"
      class="fileButton"
      @click="openModal" />
  </div>
  <BotDialog
    :isOpen="isModalOpen" 
    title="Пример диалога"
    @close="closeModal" />
</template>
<script setup>

const props = defineProps({
  openSection: {
    type: Boolean,
    default: true,
  },
});

const isModalOpen = ref(false);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const fileList = [
  {
    name: 'file_1.pdf',
  },
  {
    name: 'file_2.pdf',
  },
];
</script>
<style lang="scss" scoped>
.fileSection {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  max-width: 26rem;
  width: 33%;
  opacity: 1;
  transition: all 0.3s ease;
  padding: 0 0.8rem;

  &.closed {
    padding-right: 0;
    width: 0;
    opacity: 0;
  }

  .fileList {
    padding-top: 0.8rem;
    display: flex;
    flex-direction: column;
    gap: 0.8rem;
    max-height: calc(100dvh - 32.6rem);
    overflow-y: auto;
  }

  .file {
    position: relative;
    padding: 0 2.8rem 0 1.6rem;
    display: flex;
    gap: 0.8rem;
    align-items: center;
    width: 100%;
    min-height: 6rem;
    border-radius: 8px;
    background-color: #404E7B;
    cursor: pointer;
    transition: all 0.3s ease;

    &:hover {
      background-color: #526296;
    }

    &__name {
      width: 6.4rem;
      overflow: hidden;
      font-size: 1.6rem;
      line-height: 2rem;
    }

    &__edit {
      position: absolute;
      top: 0.8rem;
      right: 0.8rem;
      border: none;
      background: none;
      color: #A5B8F1;
    }
  }
}

.editIcon {
  position: absolute;
  top: 0.8rem;
  right: 0.8rem;
}

.fileButton {
  z-index: 1;
}
</style>