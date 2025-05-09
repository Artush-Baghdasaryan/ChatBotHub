<template>
  <div class="botInfo">
    <BotButton
      v-for="(button, index) in botButtons"
      :key="index"
      type="icon"
      :icon="button.icon"
      @click="button.click" />
    <div class="botInfo__name">{{ title }}</div>
  </div>
</template>
<script setup>
const props = defineProps({
  title: {
    type: String,
    default: 'Бот для документации',
  },
});
const emit = defineEmits(['updateFileList']);

const botButtons = [
  {
    icon: 'material-symbols--folder-outline',
    click: () => emit('updateFileList'),
  }, 
  {
    icon: 'material-symbols--edit-rounded',
  },
  {
    icon: 'mingcute--code-line',
  },
];

</script>
<style lang="scss" scoped>
.botInfo {
  margin: 0 auto;
  width: 100%;
  max-width: 86rem;
  display: flex;
  align-items: center;
  min-height: 6rem;
  padding: 0 0.8rem;
  gap: 0.4rem;
  box-shadow: 
    0 2px 6px 0px #2F395B,
    0 8px 8px -8px #2F395B,
    0 4px 6px -4px rgba(47, 57, 91, 0.4);
  z-index: 1;

  &__name {
    font-size: 2rem;
    line-height: 2.2rem;
    margin-left: 0.4rem;
  }
}
</style>