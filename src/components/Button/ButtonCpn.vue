<script setup lang="ts">
import { ref } from "vue";

const props = defineProps({
	content: { type: String, default: "Button" },
});
</script>
<template>
	<button>{{ content }}</button>
</template>
<style lang="scss" scoped></style>
