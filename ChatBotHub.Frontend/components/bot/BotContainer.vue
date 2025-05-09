<template>
  <div class="botContainer">
    <aside
      class="botContainer__sidebar"
      :class="{ 'collapsed': !isMenuOpen }">
      <div class="sidebarHeader">
        <button
          class="menuIconButton"
          @click="toggleMenu">
          <svg width="24" height="24" viewBox="0 0 24 24">
            <use xlink:href="/sprite.svg#material-symbols--menu-rounded"/>
          </svg>
        </button>
        <button
          class="menuIconButton"
          @click="() => true">
          <svg width="24" height="24" viewBox="0 0 24 24">
            <use xlink:href="/sprite.svg#material-symbols--edit-rounded"/>
          </svg>
        </button>
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
          <div class="sidebarBot__menu">
            <svg width="16" height="16" viewBox="0 0 16 16">
              <use xlink:href="/sprite.svg#charm--menu-meatball"/>
            </svg>
          </div>
        </div>
      </nav>
      <div class="sidebarFooter">
        <button
          v-for="(button, index) in sidebarButtons"
          :key="index"
          class="sidebarButton"
          :class="{ 'sidebarButton--icon': !isMenuOpen }"
          @click="() => true">
          <svg :width="sidebarButtonSize" :height="sidebarButtonSize">
            <use :xlink:href="`/sprite.svg#${button.icon}`"/>
          </svg>
          <div
            v-show="isMenuOpen"
            class="sidebarButton__title">
            {{ button.title }}
          </div>
        </button>
      </div>
    </aside>
    <main class="botContainer__main" :class="{ 'expanded': !isMenuOpen }">
      <BotContent />
    </main>
  </div>
</template>

<script setup>

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
    id: 3,
    title: 'Бот для интренет-магазина',
    date: '06.04.2025',
    files: 2,
  },
];

const sidebarButtons = [
  {
    title: 'Настройки',
    icon: 'material-symbols--settings',
  },
  {
    title: 'Частые вопросы',
    icon: 'fe--question',
  }
];

const sidebarButtonSize = computed(() => isMenuOpen.value ? '36' : '24');;

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};
</script>

<style lang="scss">
.botContainer {
  display: flex;
  min-height: 100dvh;
  position: relative;
  transition: all 0.3s ease;

  &__sidebar {
    width: 26rem;
    min-width: 26rem;
    padding: 1.2rem 1.2rem 1.2rem 2rem;
    background: #1F2A4F;
    color: white;
    position: relative;
    transition: all 0.3s ease;
    overflow: hidden;
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

  &__main {
    flex: 1;
    transition: all 0.3s ease;

    // &.expanded {
    //   // margin-left: -19rem;
    // }
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
  // overflow-y: scroll;

  .sidebarBot {
    width: 100%;
    height: 6rem;
    padding: 0.8rem;
    border-radius: 8px;
    color: #A5B8F1;
    background-color: #495b96;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    position: relative;

    &__title {
      height: 2rem;
      font-size: 1.6rem;
      line-height: 2rem;
      color: #A5B8F1;
      margin-right: 2rem;
      white-space: nowrap;
      overflow: hidden;
      // text-overflow: ellipsis;
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
      position: absolute;
      top: 0.2rem;
      right: 0.6rem;
    }
  }
}

.menuIconButton {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 3.6rem;
  width: 3.6rem;
  
  background: rgba(255, 255, 255, 0.1);
  color: white;
  border: none;
  border-radius: 8px;

  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background: rgba(255, 255, 255, 0.2);
  }

  &:active {
    transform: scale(0.95);
  }
}

.sidebarFooter {
  margin-top: auto;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.sidebarButton {
  padding: 1.2rem;
  display: flex;
  align-items: center;
  gap: 1.2rem;
  height: 6rem;
  width: 100%;
  border-radius: 8px;
  border: none;
  background: rgba(255, 255, 255, 0.1);
  color: #A5B8F1;

  &__title {
    font-size: 1.6rem;
    line-height: 2rem;
  }

  &--icon {
    padding: 0;
    justify-content: center;
    height: 3.6rem;
    width: 3.6rem;
  }
}
</style>