<script setup lang="ts">
const props = defineProps({
	type: { type: String, default: "text" },
	title: { type: String, default: null },
	placeholder: { type: String, default: null },
	tabindex: { type: Number, default: null },
});
</script>
<template>
	<label
		v-if="title"
		for="INPUT_CUSTOM_CPN"
		>{{ title }}</label
	>
	<input
		id="INPUT_CUSTOM_CPN"
		:type="type"
		:placeholder="placeholder"
		:tabindex="tabindex"
	/>
</template>
<style scoped lang="scss">
input[type="text"] {
	border: solid 1px black;
}
</style>
