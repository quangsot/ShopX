<script setup lang="ts">
import { toRef } from "vue";
import { useField } from "vee-validate";

const props = defineProps({
	type: {
		type: String,
		default: "text",
	},
	value: {
		type: String,
		default: undefined,
	},
	name: {
		type: String,
		required: true,
	},
	label: {
		type: String,
		required: true,
	},
	successMessage: {
		type: String,
		default: "",
	},
	placeholder: {
		type: String,
		default: "",
	},
});

const name = toRef(props, "name");
const {
	value: inputValue,
	errorMessage,
	meta,
} = useField(name, undefined, {
	initialValue: props.value,
});
</script>

<template>
	<div
		class="TextInput"
		:class="{ 'has-error': !!errorMessage, success: meta.valid }"
	>
		<label :for="name">{{ label }}</label>
		<input
			:name="name"
			:id="name"
			:type="type"
			:value="inputValue"
			:placeholder="placeholder"
		/>

		<p
			class="help-message"
			v-show="errorMessage || meta.valid"
		>
			{{ errorMessage || successMessage }}
		</p>
	</div>
</template>

<style scoped></style>
