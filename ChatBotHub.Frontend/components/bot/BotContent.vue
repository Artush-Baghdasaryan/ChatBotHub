<template>
  <div class="botContent">
    <BotHeader/>
    <BotInfo @updateFileList="updateFileList"/>
    <div class="chatContainer">
      <BotFileList :openSection="openFileList" />
      <BotChat />
    </div>
    <BotFooter/>
  </div>
</template>
<script setup>
const openFileList = ref(true);

const updateFileList = () => openFileList.value = !openFileList.value;

</script>
<style lang="scss" scoped>
.botContent {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100dvh;
  background-color: #2F395B;
  color: #A5B8F1;
  flex: 1;
  transition: all 0.3s ease;
}

.chatContainer {
  margin: 0 auto;
  height: 100%;
  width: 100%;
  max-width: 83.6rem;
  display: flex;
  padding: 0 0.8rem;
}
</style>