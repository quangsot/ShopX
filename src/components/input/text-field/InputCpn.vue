<script setup lang="ts">
import { ref, watch } from "vue";
import { useField } from "vee-validate";
import Icon from "@/components/icon/IconCpn.vue";
import { STATUS } from "@/helper/enum.js";

const props = defineProps({
	name: { type: String, default: "" }, // tên input dùng cho vee-validate
	type: { type: String, default: "text" }, // kiểu input textfield
	title: { type: String, default: "" }, // tiêu đề input (label)
	placeholder: { type: String, default: "" },
	tabindex: { type: Number, default: null },
	leadingIcon: { type: String, default: "" }, // font awesome icon ở đầu input
	trailingIcon: { type: String, default: "" }, // font awesome icon ở cuối input
	colorLeadingIcon: { type: String, default: "" }, // màu icon
	colorTrailingIcon: { type: String, default: "" },
	helperText: { type: String, default: "" }, // thông báo bên dưới input
});
const emit = defineEmits(["update:modelValue"]);
const statusInput = ref<STATUS>(STATUS.NORMAL);

const { value, errorMessage } = useField(() => props.name);

// biến thông báo
const infoText = ref<string>(props.helperText);

// validate input

// theo dõi thông báo lỗi sau khi validate
watch(errorMessage, (newVal) => {
	if (newVal?.length) {
		statusInput.value = STATUS.ERROR;
		infoText.value = newVal;
	} else {
		statusInput.value = STATUS.NORMAL;
		infoText.value = props.helperText;
	}
});
//bind text ra ngoài
watch(value, (newVal) => emit("update:modelValue", newVal));
</script>
<template>
	<div class="input-container">
		<label
			v-if="title"
			for="INPUT_CUSTOM_CPN"
			>{{ title }}</label
		>
		<div class="input">
			<div
				v-if="leadingIcon"
				class="leading-icon"
			>
				<Icon
					:icon="leadingIcon"
					:color="colorLeadingIcon"
				></Icon>
			</div>
			<input
				:class="{
					'has-leading-icon': leadingIcon.length > 0 && trailingIcon.length <= 0,
					'has-trailing-icon': trailingIcon.length > 0 && leadingIcon.length <= 0,
					'no-icon': trailingIcon.length <= 0 && leadingIcon.length <= 0,
					'has-both-icon': trailingIcon.length > 0 && leadingIcon.length > 0,
				}"
				id="INPUT_CUSTOM_CPN"
				:type="type"
				:placeholder="placeholder"
				:tabindex="tabindex"
				v-model="value"
			/>
			<div
				v-if="trailingIcon"
				class="trailing-icon"
			>
				<Icon
					:icon="trailingIcon"
					:color="colorTrailingIcon"
				></Icon>
			</div>
		</div>
		<div
			v-if="infoText"
			class="help-text"
			:class="{ normal: statusInput == STATUS.NORMAL, error: statusInput == STATUS.ERROR }"
		>
			{{ infoText }}
		</div>
		<!-- <font-awesome-icon icon="fa-solid fa-magnifying-glass" /> -->
	</div>
</template>
<style scoped lang="scss">
@import "text-field-style.scss";
</style>
