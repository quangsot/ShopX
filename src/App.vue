<script setup lang="ts">
import * as yup from "yup";
import { useForm } from "vee-validate";
import InputCpn from "@/components/input/text-field/InputCpn.vue";
import ButtonCpn from "./components/button/ButtonCpn.vue";
const { handleSubmit } = useForm({
	validationSchema: yup.object({
		email: yup.string().required("email không được để trống.").email("email không đúng định dạng."),
		password: yup.string().required("mật khẩu không được để trống.").min(6, "mật khẩu phải dài ít nhất 6 kí tự"),
	}),
});

const onSubmit = handleSubmit((value) => {
	console.log("submit>>>", value);
});
</script>

<template>
	<form @submit="onSubmit">
		<InputCpn
			name="email"
			title="Email"
		/>
		<InputCpn
			name="password"
			title="Password"
			type="password"
		/>
		<ButtonCpn content="Submit" />
	</form>
</template>

<style scoped lang="scss">
@import "@/assets/scss/global.scss";
form {
	width: 100%;
	height: 500px;
	display: flex;
	justify-content: center;
	align-items: center;
	flex-direction: column;
}
</style>
