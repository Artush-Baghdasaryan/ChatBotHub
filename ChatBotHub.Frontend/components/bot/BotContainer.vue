<template>
  <div class="botContainer">
    <BotSidebar v-show="!width.isTablet" />
    <BotContent />
  </div>
</template>

<script setup>
import useWindowSize from '@/composables/useWindowSize'

const { width } = useWindowSize()
</script>

<style lang="scss">
.botContainer {
  display: flex;
  min-height: 100dvh;
  position: relative;
  transition: all 0.3s ease;
}
</style>