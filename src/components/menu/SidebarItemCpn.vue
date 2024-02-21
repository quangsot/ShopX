<script setup lang="ts">
const props = defineProps({
	icon: { type: String, default: "" },
	text: { type: String, default: "Sidebar Item" },
	numberNotice: { type: Number, default: 0 },
});
</script>
<template>
	<div class="sidebar-item">
		<font-awesome-icon
			:icon="icon"
			class="icon"
		/>
		<p>{{ text }}</p>
		<div
			v-if="numberNotice"
			class="notice"
		>
			{{ numberNotice }}
		</div>
	</div>
</template>
<style scoped lang="scss">
// @import "../../assets/scss/index.css";s
.sidebar-item {
	display: flex;
	align-items: center;
	justify-content: start;
	gap: 12px;
	height: 56px;
	width: 100%;
	color: var(--color-info-dark);
	transition: all 0.3s ease;
	cursor: pointer;
	&:hover {
		padding-left: 8px;
		color: var(--color-main);
	}
	.icon {
		width: 20px;
		height: 20px;
	}
	p {
		font-size: 14px;
		font-weight: bold;
	}
	.notice {
		width: fit-content;
		height: 20px;
		border: none;
		border-radius: var(--border-radius-1);
		background-color: #ff0000;
		color: var(--color-white);
		padding: 0 4px;
	}
}
</style>
