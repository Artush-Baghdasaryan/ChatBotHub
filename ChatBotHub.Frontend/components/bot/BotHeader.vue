<template>
  <header class="botHeader">
    <div class="logo">RAG AI Bot</div>
    <div class="user">
      <div class="user__name">{{ user }}</div>
      <div class="user__icon">
        <svg width="20" height="20" viewBox="0 0 20 20">
          <use xlink:href="/sprite.svg#mdi--user"/>
        </svg>
      </div>
    </div>
  </header>
</template>
<script setup>
const props = defineProps({
  user: {
    type: String,
    default: 'user',
  },
});
</script>
<style lang="scss" scoped>
.botHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  min-height: 6rem;
  padding-inline: 2rem;
  border-bottom: 2px solid #404E7B;
}

.logo {
  font-size: 2.4rem;
  font-weight: 900;
  line-height: 2.8rem;
  cursor: pointer;
}

.user {
  display: flex;
  align-items: center;
  gap: 0.8rem;

  &__name {
    font-size: 1.6rem;
    line-height: 1.8rem;
  }

  &__icon {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 3.6rem;
    width: 3.6rem;
    border-radius: 50%;
    background-color: #4C5B8D;
  }
}
</style>