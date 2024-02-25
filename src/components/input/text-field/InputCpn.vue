<script setup lang="ts">
import { computed, ref, watch, type InputHTMLAttributes } from "vue";
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

const statusInput = ref<STATUS>(STATUS.NORMAL);
var valueInput = ref<unknown>();
var errorMessage = ref<string | undefined>("");
({ value: valueInput, errorMessage } = useField(() => {
	if (props.name) return props.name;
	else return "";
}));
/**
 * hàm khởi tạo
 */
const created = () => {
	console.log(props.helperText);

	// khi khởi tạo nếu useField không trả về giá trị gì cho valueInput thì gán mặc định là modelValue
	if (!valueInput.value) valueInput.value = props.modelValue;
	else {
	}
};
created();
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
	<div class="input-container">
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
				v-model="valueInput"
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
