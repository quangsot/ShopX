<script setup lang="ts">
import ComboboxCpn from "./components/input/ComboboxCpn.vue";
import { Form } from "vee-validate";
import * as yup from "yup";
</script>

<template>
	<RouterView></RouterView>
	<div class="test">
		<ComboboxCpn
			title="title"
			name="name"
		/>
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
