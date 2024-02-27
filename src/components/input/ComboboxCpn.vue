<script setup lang="ts">
import InputCpn from "./text-field/InputCpn.vue";
import { ref, type PropType, watch, computed, onMounted } from "vue";

const props = defineProps({
	values: { type: Array as PropType<string[]>, default: [] },
	displays: { type: Array as PropType<string[]>, default: [] },
	name: { type: String, default: "" },
	title: { type: String, default: "" },
	helperText: { type: String, default: "" },
	placeholder: { type: String, default: "" },
	tabindex: { type: Number, default: null },
	leadingIcon: { type: String, default: "" }, // font awesome icon ở đầu input
	trailingIcon: { type: String, default: "" }, // font awesome icon ở cuối input
	colorLeadingIcon: { type: String, default: "" }, // màu icon
	colorTrailingIcon: { type: String, default: "" },
	validate: { type: Boolean, default: true },
	autoSelect: { type: Boolean, default: false },
	readonly: { type: Boolean, default: false },
	menuPosition: { type: String, default: "bottom" }, // bottom || top
});

const emit = defineEmits(["getItemSelected"]);
const valueCombobox = ref<string>("");
const listDisplay = ref<string[]>(props.displays);
const selectedItem = ref<[string, string]>(["", ""]);
const isDisplayList = ref<boolean>(false);
// index của li dùng cho việc ấn lên xuống
const indexOfLi = ref<number>(-1);
onMounted(() => {
	if (props.autoSelect) {
		valueCombobox.value = props.displays[0];
		selectedItem.value[0] = props.values[0];
		selectedItem.value[1] = props.displays[0];
		indexOfLi.value = 0;
	}
});
// tính toán index item được chọn dựa trên valueCombobox
const indexItemSelected = computed(() => {
	return props.displays.findIndex((item) => item == valueCombobox.value);
});
// tính giá trị item theo index của item
const valueSelectedItem = computed(() => props.values[indexItemSelected.value]);
/**
 * ẩn danh sách item
 */
const handleHideListItem = () => {
	indexOfLi.value = -1;
	isDisplayList.value = false;
	listDisplay.value = props.displays;
};
/**
 * hiển thị danh sách item
 */
const handleDisplayListItem = () => {
	isDisplayList.value = true;
};
/**
 * toggle list item
 */
const handleToggleListItem = () => {
	isDisplayList.value = !isDisplayList.value;
};
/**
 * sử lý sự kiện chọn 1 phần tử trên list item bằng enter
 */
const handleSelectItemByEnter = (): void => {
	// người dùng chọn phần tử bằng phím lên xuống
	if (indexOfLi.value >= 0) {
		let itemSelected = listDisplay.value[indexOfLi.value];
		valueCombobox.value = itemSelected;
		selectedItem.value[0] = valueSelectedItem.value;
		selectedItem.value[1] = itemSelected;
	}
	handleHideListItem();
};
/**
 * xử lý sự kiện chọn 1 phần tử trên list item bằng click chuột
 */
const handleSelectItemByClick = (item: string, index: number): void => {
	valueCombobox.value = item;
	selectedItem.value[0] = valueSelectedItem.value;
	selectedItem.value[1] = item;
	handleHideListItem();
};

const liElements = ref<HTMLLIElement[] | null>(null);

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
	} else return;
};

/**
 * search theo input
 */
watch(valueCombobox, (newVal) => {
	// ==========tìm kiếm==========//
	if (!props.readonly) {
		// nếu không phải trạng thái đọc thì được tìm kiếm
		if (newVal) {
			let searchList = props.displays.filter((item) => item.includes(newVal));
			if (searchList) listDisplay.value = searchList;
			else listDisplay.value = props.displays;
		} else {
			listDisplay.value = props.displays;
		}
		indexOfLi.value = -1;
		handleDisplayListItem();
	}

	// bind dữ liệu ra ngoài
	if (newVal && indexItemSelected.value > -1) {
		selectedItem.value[0] = valueSelectedItem.value;
		selectedItem.value[1] = valueCombobox.value;
	} else {
		selectedItem.value[0] = "";
		selectedItem.value[1] = "";
	}
	emit("getItemSelected", selectedItem.value);
});
</script>
<template>
	<div
		v-out-side="handleHideListItem"
		class="combobox-container"
	>
		<InputCpn
			:validate="validate"
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
			:readonly="readonly"
			@on-focus="handleDisplayListItem"
			@on-blur="
				($event) => {
					if ($event.relatedTarget?.stagName?.toLowerCase() !== 'li') handleHideListItem();
				}
			"
			@page-down="handlePageDown"
			@page-up="handlePageUp"
			@enter="handleSelectItemByEnter"
			@shift-enter="handleDisplayListItem"
			@click-trailing-icon="handleToggleListItem"
			v-out-side="handleHideListItem"
		/>
		<div
			v-if="isDisplayList"
			class="list-item"
			v-out-side="handleHideListItem"
			:class="{ 'bottom-menu': menuPosition == 'bottom', 'top-menu': menuPosition == 'top' }"
		>
			<ul>
				<li
					ref="liElements"
					v-for="(item, index) in listDisplay"
					:key="item"
					:class="{ 'active-item': item == selectedItem[1], 'hover-item': index == indexOfLi }"
					@mousedown="handleSelectItemByClick(item, index)"
					@blur="($event: FocusEvent) => {
						console.log('blur li');
						const relatedEl = $event.relatedTarget as HTMLElement;
						if (relatedEl == null || relatedEl.tagName.toLowerCase() !== 'li'){
							handleHideListItem();
						}
				}"
				>
					{{ item }}
					<div v-if="item == selectedItem[1]">
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
.top-menu {
	position: absolute;
	bottom: 45px;
	left: 0;
}
.bottom-menu {
	position: absolute;
	top: 100px;
	left: 0;
}
</style>
