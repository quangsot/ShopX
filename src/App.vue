<script setup lang="ts">
import Input from "@/components/Input/InputCpn.vue";
</script>

<template>
	<RouterView></RouterView>
	<div class="test">
		<Input type="text"></Input>
	</div>
</template>

<style scoped lang="scss">
@import "@/assets/scss/global.scss";
.test {
	width: 100%;
	height: 500px;
	display: flex;
	justify-content: center;
	align-items: center;
}
</style>
