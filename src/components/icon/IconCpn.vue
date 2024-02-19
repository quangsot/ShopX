<script setup lang="ts">
const props = defineProps({
	color: { type: String, default: "" },
	icon: { type: String, default: "fa-regular fa-face-smile" },
});
</script>
<template>
	<font-awesome-icon
		:icon="icon"
		:class="{ 'color-icon': color.length > 0 }"
	/>
</template>
<style scoped>
.color-icon {
	color: v-bind(props.color);
}
</style>
