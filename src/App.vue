<script setup lang="ts">
import ComboboxCpn from "./components/input/ComboboxCpn.vue";
</script>

<template>
	<RouterView></RouterView>
	<div class="test">
		<ComboboxCpn title="title" />
	</div>
</template>

<style scoped lang="scss">
@import "@/assets/scss/global.scss";
.test {
	height: 500px;
	display: flex;
	align-items: center;
	justify-content: center;
}
</style>
