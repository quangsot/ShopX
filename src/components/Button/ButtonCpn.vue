<script setup lang="ts">
import { ref } from "vue";

const emit = defineEmits(["clickButton"]);

const props = defineProps({
	type: { type: String, default: "primary" },
	content: { type: String, default: "Button" },
});
</script>
<template>
	<button
		@click="() => emit('clickButton')"
		:class="type"
	>
		{{ content }}
	</button>
</template>
<style lang="scss" scoped>
button {
	transition: box-shadow 0.2s, -ms-transform 0.1s, -webkit-transform 0.1s, transform 0.1s;
	cursor: pointer;
	user-select: none;
	&:focus-visible {
		box-shadow: var(--color-blue-300) 0 0 7px 4px;
		transition: box-shadow 0.2s;
	}
	&:active {
		transform: scale(0.96);
	}
}
.primary {
	width: 80px;
	height: 36px;
	background-color: var(--color-main);
	border-radius: var(--border-radius-1);
	color: var(--color-white);
	&:hover {
		background-color: var(--color-blue-500);
	}
}
.extra {
	width: 80px;
	height: 36px;
	background-color: var(--color-white);
	border: solid 1px var(--color-main);
	border-radius: var(--border-radius-1);
	color: var(--color-main);
	&:hover {
		background-color: var(--color-background);
	}
}
</style>
