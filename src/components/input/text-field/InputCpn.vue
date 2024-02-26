<script setup lang="ts">
import { computed, ref, watch, toRef } from "vue";
import { useField } from "vee-validate";
import Icon from "@/components/icon/IconCpn.vue";
import { STATUS } from "@/helper/enum.js";

const props = defineProps({
	modelValue: { type: String, default: "" },
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
const emit = defineEmits([
	"update:modelValue",
	"onFocus",
	"onFocusin",
	"onFocusout",
	"onBlur",
	"pageDown",
	"pageUp",
	"enter",
	"shift-enter",
	"onInput",
]);
const name = toRef(props, "name");
const statusInput = ref<STATUS>(STATUS.NORMAL);

const { value: valueInput, errorMessage, meta } = useField(name, undefined);

// debugger;

// biến thông báo
const infoText = ref<string>(props.helperText);

watch(
	() => props.helperText,
	(newVal) => {
		if (newVal) {
			infoText.value = newVal;
		} else {
			infoText.value = "";
		}
	}
);

// theo dõi thông báo lỗi sau khi validate
watch(errorMessage, (newVal) => {
	// debugger;
	console.log("errorMsg>>>", newVal);
	if (newVal && newVal.length > 0) {
		statusInput.value = STATUS.ERROR;
		infoText.value = newVal;
	} else {
		statusInput.value = STATUS.NORMAL;
		infoText.value = props.helperText;
	}
});

//bind vào
watch(
	() => props.modelValue,
	(newVal) => {
		if (newVal) {
			valueInput.value = newVal;
		} else {
			valueInput.value = "";
		}
	}
);

//bind text ra ngoài
watch(valueInput, (newVal) => emit("update:modelValue", newVal));
</script>
<template>
	<div
		class="input-container"
		:class="{ normal: statusInput == STATUS.NORMAL, error: statusInput == STATUS.ERROR, success: meta.valid }"
	>
		<label
			v-if="title"
			:for="`${$.uid}`"
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
				:name="name"
				v-model="valueInput"
				:class="{
					'has-leading-icon': leadingIcon.length > 0 && trailingIcon.length <= 0,
					'has-trailing-icon': trailingIcon.length > 0 && leadingIcon.length <= 0,
					'no-icon': trailingIcon.length <= 0 && leadingIcon.length <= 0,
					'has-both-icon': trailingIcon.length > 0 && leadingIcon.length > 0,
				}"
				:id="`${$.uid}`"
				:type="type"
				:placeholder="placeholder"
				:tabindex="tabindex"
				@focus="(e: FocusEvent)=>$emit('onFocus',e)"
				@focusin="(e: FocusEvent)=>$emit('onFocusin',e)"
				@focusout="(e:FocusEvent)=>$emit('onFocusout',e)"
				@blur="(e: FocusEvent)=>$emit('onBlur',e)"
				@keyup.down="(e: KeyboardEvent)=>($emit('pageDown',e))"
				@keyup.up="(e: KeyboardEvent)=>($emit('pageUp',e))"
				@keydown.enter="(e: KeyboardEvent)=>($emit('enter',e))"
				@keydown.enter.shift.exact.prevent="(e: KeyboardEvent)=>($emit('shift-enter',e))"
				@input="(e: Event)=>($emit('onInput'),e as InputEvent)"
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
			class="help-text"
			v-if="infoText"
		>
			{{ infoText }}
		</div>
	</div>
</template>
<style scoped lang="scss">
@import "text-field-style.scss";
</style>
