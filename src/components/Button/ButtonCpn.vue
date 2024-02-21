<script setup lang="ts">
import { ref } from "vue";

const emit = defineEmits(["clickButton"]);

const props = defineProps({
	type: { type: String, default: "primary" }, // primary, extra
	content: { type: String, default: "Button" },
	icon: { type: String, default: "" },
});
</script>
<template>
	<button
		@click="() => emit('clickButton')"
		:class="type"
	>
		<IconCpn
			class="icon"
			:icon="icon"
		></IconCpn>
		{{ content }}
	</button>
</template>
<style lang="scss" scoped>
button {
	transition: box-shadow 0.2s, -ms-transform 0.1s, -webkit-transform 0.1s, transform 0.1s;
	cursor: pointer;
	user-select: none;
	display: flex;
	align-items: center;
	justify-content: center;
	&:focus-visible {
		box-shadow: var(--color-blue-300) 0 0 7px 4px;
		transition: box-shadow 0.2s;
	}
	&:active {
		transform: scale(0.96);
	}
	.icon {
		margin-right: 8px;
		width: 20px;
		height: 20px;
	}
}
.primary {
	min-width: 80px;
	width: fit-content;
	height: 36px;
	background-color: var(--color-main);
	border-radius: var(--border-radius-1);
	color: var(--color-white);
	&:hover {
		background-color: var(--color-blue-500);
	}
}
.extra {
	min-width: 80px;
	width: fit-content;
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
