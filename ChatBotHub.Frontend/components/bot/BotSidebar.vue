<template>
  <aside
    class="botSidebar"
    :class="{ 'collapsed': !isMenuOpen }">
    <div class="sidebarHeader">
      <BotButton
        buttonType="icon"
        icon="material-symbols--menu-rounded"
        @click="toggleMenu" />
      <BotButton
        buttonType="icon"
        icon="material-symbols--edit-rounded"
        @click="openModal" />
    </div>
    <nav class="sidebarList">
      <div
        v-for="item in botList" 
        :key="item.id"
        class="sidebarBot">
        <div class="sidebarBot__title">{{ item.title }}</div>
        <div class="sidebarBot__info">
          <div class="sidebarBot__date">{{ item.date }}</div>
          <div class="sidebarBot__files">
            <div class="sidebarBot__count">{{ item.files }}</div>
            <svg width="16" height="16" viewBox="0 0 16 16">
              <use xlink:href="/sprite.svg#mdi--file"/>
            </svg>
          </div>
        </div>
        <!-- <BotButton
          buttonType="icon-info"
          icon="charm--menu-meatball"
          class="sidebarBot__menu" 
          @click="() => 1"/> -->
      </div>
    </nav>
    <div class="sidebarFooter">
      <BotButton
        :buttonType="!isMenuOpen ? 'icon' : 'icon-full'"
        title="Настройки"
        icon="material-symbols--settings"
        @click="openModal" />
      <BotButton
        :buttonType="!isMenuOpen ? 'icon' : 'icon-full'"
        title="Частые вопросы"
        icon="fe--question"
        @click="openModal" />
    </div>
    <BotDialog
      :isOpen="isModalOpen" 
      title="Пример диалога"
      @close="closeModal" />
  </aside>
</template>

<script setup>

const isModalOpen = ref(false);

const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

const isMenuOpen = ref(true);
const botList = [
  {
    id: 1,
    title: 'Бот для документации',
    date: '19.03.2025',
    files: 2,
  },
  {
    id: 2,
    title: 'Бот для tg канала',
    date: '25.02.2025',
    files: 1,
  },
  {
    id: 3,
    title: 'Бот: помощь по доставке',
    date: '18.02.2025',
    files: 4,
  },
  {
    id: 4,
    title: 'Бот для интренет-магазина',
    date: '06.04.2025',
    files: 2,
  },
];

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};
</script>

<style lang="scss" scoped>
.botSidebar {
  width: 26rem;
  min-width: 26rem;
  padding: 1.2rem 1.2rem 1.2rem 2rem;
  background-color: #1F2A4F;
  color: white;
  position: relative;
  transition: all 0.3s ease;
  display: flex;
  flex-direction: column;

  &.collapsed {
    width: 6rem;
    min-width: 6rem;
    padding: 1.2rem;

    .sidebarList {
      display: none;
    }

    .sidebarHeader {
      gap: 1.2rem;
      flex-direction: column;
    }
  }
}

.sidebarHeader {
  display: flex;
  justify-content: space-between;
  padding-bottom: 1.2rem;
}

.sidebarList {
  margin-top: 6rem;
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  max-height: 40rem;
  height: 60rem;
  width: 100%;
  max-height: calc(100dvh - 40.6rem);
  overflow-y: auto;

  &::-webkit-scrollbar-track {
    background-color: #2f395b00;
  }
}

.sidebarBot {
  width: 100%;
  height: 6rem;
  padding: 0.8rem;
  border-radius: 8px;
  color: #A5B8F1;
  background-color: #2C3963;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  position: relative;
  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background-color: #404E7B;

    .sidebarBot__menu {
      display: inline-block;
    }
  }

  &:active{
    background-color: #495b96;
  }

  &__title {
    height: 2rem;
    font-size: 1.6rem;
    line-height: 2rem;
    color: #A5B8F1;
    margin-right: 2rem;
    white-space: nowrap;
    overflow: hidden;
  }

  &__info {
    display: flex;
    justify-content: space-between;
  }

  &__date {
    font-size: 1.2rem;
    line-height: 1.6rem;
  }

  &__files {
    display: flex;
    flex-direction: row;
    gap: 0.2rem;
  }

  &__count {
    font-size: 1.2rem;
    line-height: 1.6rem;
  }
  
  &__menu {
    display: none;
    position: absolute;
    top: 0.2rem;
    right: 0.6rem;
    z-index: 2;
  }
  
  // &__menuList {
  //   position: absolute;
  //   top: 1.6rem;
  //   right: -6rem;
  //   padding: 0.8rem;
  //   border-radius: 8px;
  //   background-color: #2d3e6f;
  //   list-style-type: none;
  //   z-index: 20;
  // }
}

.sidebarFooter {
  margin-top: auto;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}
</style>