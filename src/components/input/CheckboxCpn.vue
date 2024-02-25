<script setup lang="ts">
import { ref, watch, type PropType } from "vue";

const props = defineProps({
	title: { type: String, default: "" },
	value: { type: String, default: "checkbox default value" },
	indeterminate: { type: Boolean, default: false },
});
const emit = defineEmits(["checked"]);
const isChecked = defineModel();
watch(isChecked, (newVal) => {
	if (newVal) {
		emit("checked", [true, props.value]);
	} else {
		emit("checked", [false, props.value]);
	}
});
</script>
<template>
	<div class="checkbox-container">
		<input
			hidden
			type="checkbox"
			:id="`${$.uid}`"
			v-model="isChecked"
		/>
		<div
			@click="isChecked = !isChecked"
			class="checkbox-wraper"
			:class="{ checked: isChecked }"
		>
			<div class="checkbox">
				<font-awesome-icon
					v-if="isChecked && !indeterminate"
					style="color: var(--color-white)"
					:icon="['fas', 'check']"
				/>
				<font-awesome-icon
					v-if="isChecked && indeterminate"
					style="color: var(--color-white)"
					:icon="['fas', 'minus']"
				/>
			</div>
		</div>
		<label
			:for="`${$.uid}`"
			v-if="title"
			>{{ title }}</label
		>
	</div>
</template>
<style scoped lang="scss">
.checkbox-container {
	display: flex;
	align-items: center;
	label,
	.checkbox-wraper {
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
		user-select: none;
	}
	.checkbox-wraper {
		width: 45px;
		height: 45px;
		border: none;
		border-radius: 100%;
		background-color: transparent;
		transition: all 0.1s ease-in-out;
		&:hover {
			// box-shadow: 0 0 0 15px var(--color-grey-200);
			background-color: var(--color-grey-300);
		}
		&:active {
			// box-shadow: 0 0 0 15px var(--color-blue-200);
			background-color: var(--color-blue-50);
			border-color: var(--color-blue-50);
		}
		.checkbox {
			width: 20px;
			height: 20px;
			border: solid 1px var(--color-info-dark);
			border-radius: 4px;
			cursor: pointer;
			display: flex;
			align-items: center;
			justify-content: center;
			// background-color: transparent;
		}
	}
}
.checked {
	&:hover {
		// box-shadow: 0 0 0 15px var(--color-grey-200);
		background-color: var(--color-blue-50) !important;
	}
	.checkbox {
		background-color: var(--color-main);
		border: none !important;
	}
}
</style>
