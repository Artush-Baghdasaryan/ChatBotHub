<template>
  <footer class="botContent__footer">
		<div class="inputSection">
			<textarea
				class="queryInput"
				placeholder="Введите ваш запрос..."
				rows="3"
				@keydown.enter.exact.prevent="handleSubmit">
			</textarea>
			<div class="inputSection__buttons">
				<BotButton
					type="outlined"
					title="Удалить чат"
					icon="material-symbols--delete-outline-rounded" />
				<BotButton
					type="outlined"
					title="Придумать"
					icon="bi--stars" />
			</div>
			<BotButton
				type="icon-round"
				icon="teenyicons--send-outline"
				class="submitButton"
				@click="handleSubmit" />
		</div>
		<div class="underText">Platform for creating AI bots with RAG</div>
	</footer>
</template>
<script setup>
const handleSubmit = () => {
  if (queryText.value.trim()) {
    console.log('Отправлен запрос:', queryText.value);
    // Здесь будет логика отправки запроса
    queryText.value = '';
  }
};

</script>
<style lang="scss" scoped>
.botContent__footer {
  margin-top: auto;
  width: 100%;
  max-width: 86rem;
  margin-inline: auto;
  padding: 0 0.8rem;
  box-shadow: 
    0 -2px 6px 0px #2F395B,
    0 -8px 8px -8px #2F395B,
    0 -4px 6px -4px rgba(47, 57, 91, 0.4);
}

.inputSection {
  position: relative;
  max-width: 86rem;
  margin-top: 0.8rem;
  height: 10.6rem;
  border-radius: 2.4rem;
  color: #A5B8F1;
  background-color: #4B5988;
  padding: 1.2rem;

  &__buttons {
    display: flex;
    gap: 0.8rem;
    padding-block: 0.4rem;
  }
}

.queryInput {
  color: #A5B8F1;
  background-color: #4B5988;
  background: none;
  width: 100%;
  border: none;
  resize: none;
  font-size: 1.6rem;
  line-height: 2rem;
  height: 5rem;
  transition: all 0.2s;

  &::placeholder {
    color: #A5B8F1;
  }

  &:focus {
    outline: none;
  }

	&::-webkit-scrollbar-track {
    background-color: #2f395b00;
  }

  &::-webkit-scrollbar-thumb {
    background-color: #A5B8F1;

    &:hover {
      background-color: #acbef2;
    }

    &:active {
      background-color: #d7dff6;
    }
  }
}

.submitButton {
	right: 0.8rem;
	bottom: 0.8rem;
}

.underText {
  height: 3.6rem;
  text-align: center;
  color: #64748b;
  font-size: 1.2rem;
  line-height: 1.6rem;
  font-weight: 200;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #A5B8F1;
}
</style>