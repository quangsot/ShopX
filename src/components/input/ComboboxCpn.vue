<script setup lang="ts">
import InputCpn from "./text-field/InputCpn.vue";
import { ref, type PropType, watch } from "vue";
const props = defineProps({
	values: { type: Array as PropType<string[]>, default: ["1", "2", "3"] },
	displays: { type: Array as PropType<string[]>, default: ["số 1", "số 2", "số 3", "số 3", "số 3", "số 3"] },
	name: { type: String, default: "" },
	title: { type: String, default: "combobox title" },
	helperText: { type: String, default: "" },
	placeholder: { type: String, default: "" },
	tabindex: { type: Number, default: null },
	leadingIcon: { type: String, default: "" }, // font awesome icon ở đầu input
	trailingIcon: { type: String, default: "" }, // font awesome icon ở cuối input
	colorLeadingIcon: { type: String, default: "" }, // màu icon
	colorTrailingIcon: { type: String, default: "" },
});
const valueCombobox = ref<string>("");
const tempValueCombobox = ref<string>("");
const listDisplay = ref<string[]>(props.displays);
const indexItemSelected = ref<number>(-1);
const selectedItem = ref<[string, string]>(["", ""]);
const isDisplayList = ref<boolean>(false);

/**
 * ẩn danh sách item
 */
const handleHideListItem = () => {
	// indexOfLi.value = -1;
	isDisplayList.value = false;
	listDisplay.value = props.displays;
};
/**
 * sử lý sự kiện chọn 1 phần tử trên list item bằng enter
 */
const handleSelectItem = (): void => {
	// người dùng chọn phần tử bằng phím lên xuống
	if (indexOfLi.value >= 0) {
		selectedItem.value[0] = props.values[indexOfLi.value];
		selectedItem.value[1] = props.displays[indexOfLi.value];
		valueCombobox.value = tempValueCombobox.value;
	}
	// người dùng không chọn bằng phím lên xuống
	// người dùng nhập trực tiếp
	else {
		indexItemSelected.value = getIndexSelected();
		if (indexItemSelected.value > -1) {
			selectedItem.value[0] = `${indexItemSelected.value}`;
			selectedItem.value[1] = valueCombobox.value;
		} else {
			selectedItem.value[0] = "";
			selectedItem.value[1] = "";
		}
	}
};
/**
 * xử lý sự kiện chọn 1 phần tử trên list item bằng click chuột
 */
const handleClickSelectItem = (item: string, index: number): void => {
	indexItemSelected.value = getIndexSelected();
	indexOfLi.value = indexItemSelected.value;
	selectedItem.value[0] = props.values[indexItemSelected.value];
	selectedItem.value[1] = item;
	valueCombobox.value = item;
};

const liElements = ref<HTMLLIElement[] | null>(null);

// index của li dùng cho việc ấn lên xuống
let indexOfLi = ref<number>(-1);
/**
 * xử lý ấn phím xuống để chọn phần tử bên dưới
 * @param e sự kiện của bàn phím
 */
const handlePageDown = (e: KeyboardEvent): void => {
	if (!isDisplayList.value) isDisplayList.value = true;
	if (liElements.value && isDisplayList.value == true) {
		// debugger;
		if (indexOfLi.value >= liElements.value.length - 1) {
			indexOfLi.value = 0;
		} else {
			indexOfLi.value++;
		}
		// liElements.value[indexOfLi.value].focus();
		tempValueCombobox.value = listDisplay.value[indexOfLi.value];
	} else return;
};
/**
 * xử lý ấn phím lên để chọn phần tử bên trên
 * @param e sự kiện của bàn phím
 */
const handlePageUp = (e: KeyboardEvent): void => {
	if (liElements.value && isDisplayList.value == true) {
		if (indexOfLi.value <= 0) {
			indexOfLi.value = liElements.value.length - 1;
		} else {
			indexOfLi.value--;
		}
		// liElements.value[indexOfLi.value].focus();
		tempValueCombobox.value = listDisplay.value[indexOfLi.value];
	} else return;
};

/**
 * search theo input
 */
watch(valueCombobox, (newVal) => {
	// ==========tìm kiếm==========//
	if (newVal) {
		let searchList = props.displays.filter((item) => item.includes(newVal));
		if (searchList) listDisplay.value = searchList;
		else listDisplay.value = props.displays;
	} else {
		listDisplay.value = props.displays;
	}
	indexOfLi.value = -1;
});

/**
 * lấy index của phần tử được chọn trong props.display
 */
const getIndexSelected = (): number => {
	let index = props.displays.findIndex((item) => item == valueCombobox.value);
	return index;
};
</script>
<template>
	<div class="combobox-container">
		<InputCpn
			v-model:model-value="valueCombobox"
			:name="name"
			type="text"
			:title="title"
			:placeholder="placeholder"
			:tabindex="tabindex"
			:leading-icon="leadingIcon"
			:trailing-icon="trailingIcon"
			:color-leading-icon="colorLeadingIcon"
			:color-trailing-icon="colorTrailingIcon"
			@on-focus="
				() => {
					isDisplayList = true;
				}
			"
			@on-blur="
				($event) => {
					if ($event.relatedTarget?.stagName?.toLowerCase() !== 'li') handleHideListItem();
				}
			"
			@page-down="handlePageDown"
			@page-up="handlePageUp"
			@enter="
				() => {
					handleSelectItem();
					handleHideListItem();
					// validateValueCombobox();
				}
			"
			@shift-enter="if (!isDisplayList) isDisplayList = true;"
			@on-input="if (!isDisplayList) isDisplayList = true;"
		/>
		<div
			v-if="isDisplayList"
			class="list-item"
			v-out-side="handleHideListItem"
		>
			<ul>
				<li
					ref="liElements"
					v-for="(item, index) in listDisplay"
					:key="item"
					:class="{ 'active-item': index == indexItemSelected, 'hover-item': index == indexOfLi }"
					@mousedown="handleClickSelectItem(item, index)"
					@blur="($event: FocusEvent) => {
						console.log('blur li');
						const relatedEl = $event.relatedTarget as HTMLElement;
						if (relatedEl == null || relatedEl.tagName.toLowerCase() !== 'li'){
							handleHideListItem();
						}
				}"
				>
					{{ item }}
					<div>
						<font-awesome-icon
							style="color: var(--color-main)"
							icon="fa-regular fa-circle-check"
						/>
					</div>
				</li>
			</ul>
		</div>
	</div>
</template>
<style scoped lang="scss">
.combobox-container {
	position: relative;
	.list-item {
		position: absolute;
		top: 80px;
		left: 0;
		width: 100%;
		background-color: var(--color-white);
		border-radius: var(--border-radius-1);
		box-shadow: 0 0 40px 10px var(--color-grey-100);
		transition: all 0.3s ease;
		max-height: 300px;
		height: fit-content;
		overflow: auto;
		padding-bottom: 4px;
		&:hover {
			box-shadow: 0 0 10px 2.5px var(--color-grey-200);
		}
		ul {
			width: 100%;
			border: none;
			background-color: transparent;
			li {
				margin: 4px 0;
				padding-left: 8px;
				height: 32px;
				width: 100%;
				// border-radius: var(--border-radius-1);
				border: none;
				display: flex;
				align-items: center;
				justify-content: start;
				position: relative;
				cursor: pointer;

				&:hover {
					transition: all 0.3s ease;
					background-color: var(--color-blue-50);
				}
				&:active {
					background-color: var(--color-blue-100);
				}
				div {
					width: 32px;
					height: 32px;
					display: none;
					align-items: center;
					justify-content: center;
					position: absolute;
					top: 0;
					right: 0;
				}
				&:focus {
					background-color: var(--color-blue-100);
				}
			}
		}
	}
}
.active-item {
	background-color: var(--color-blue-100);
	div {
		display: flex !important;
	}
}
.hover-item {
	transition: all 0.3s ease;
	background-color: var(--color-blue-50);
}
</style>
