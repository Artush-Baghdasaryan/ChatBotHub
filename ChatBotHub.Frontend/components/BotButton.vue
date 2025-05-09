<template>
  <button class="button" :class="type">
    <svg
      class="button__icon"
      :width="iconSize[type]"
      :height="iconSize[type]">
      <use :xlink:href="`/sprite.svg#${icon}`"/>
    </svg>
    <div
      v-show="!['icon', 'icon-round'].includes(type)"
      class="button__title">
      {{ title }}
    </div>
  </button>
</template>

<script setup>
const props = defineProps({
  title: {
    type: String,
  },
  icon: {
    type: String,
  },
  type: {
    type: String,
    validator: (value) => ['icon', 'icon-round', 'icon-full', 'icon-full-center', 'icon-info', 'outlined', 'link', 'fulled'].includes(value),
  },
});

const iconSize = {
  'icon': 24, //
  'icon-round': 20, //
  'icon-full': 36, //
  'icon-info': 16,
  'icon-full-center': 24,
  'outlined': 16, //
  'link': 20,
  'fulled': 24, 
};
</script>

<style lang="scss" scoped>
.button {
  border: none;
  display: flex;
  align-items: center;
  cursor: pointer;
  transition: all 0.3s ease;

  &.icon {
    justify-content: center;
    height: 3.6rem;
    width: 3.6rem;
    background-color: rgba(59, 75, 126, 0.5);
    color: #A5B8F1;
    border-radius: 8px;

    &:hover {
      background-color: #404E7B;
    }

    &:active {
      background-color: #404E7B;
      transform: scale(0.95);
    }
  }

  &.icon-round {
    justify-content: center;
    height: 3.2rem;
    width: 3.2rem;
    position: absolute;
    background-color: #A5B8F1;
    color: #6366f1;
    border-radius: 50%;

    &:hover {
      color: #4f46e5;
      transform: translateY(-1px);
    }

    &:active {
      transform: translateY(0);
    }
  }

  &.icon-full {
    padding: 0 1.2rem;
    gap: 1.2rem;
    height: 6rem;
    width: 100%;
    border-radius: 8px;
    background-color: #2C3963;
    color: #A5B8F1;
    font-size: 1.6rem;
    line-height: 2rem;
    
    &:hover {
      background-color: #404E7B;
    }

    &:active {
      background-color: #404E7B;
      transform: scale(0.95);
    }
  }

  &.icon-full-center {
    @extend .icon-full;
    height: 4.8rem;
    justify-content: center;
    background-color: #404E7B;

    &:hover {
      background-color: #4B5988;
    }
  }

  &.icon-info {
    background-color: #ffffff00;
    color: #A5B8F1;
  }

  &.outlined {
    display: flex;
    gap: 0.4rem;
    padding: 0 0.6rem;
    background: none;
    height: 2.4rem;
    border-radius: 1.2rem;
    font-size: 1.2rem;
    color: #A5B8F1;
    border: 0.1rem solid #A5B8F1;
  }
}
</style>