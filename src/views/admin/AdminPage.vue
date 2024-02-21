<script setup lang="ts">
import TheBodyAdmin from "./layout/TheBodyAdmin.vue";
import TheSidebarAdmin from "./layout/TheSidebarAdmin.vue";
import TheHeaderAdmin from "./layout/TheHeaderAdmin.vue";
</script>
<template>
	<div class="admin-page">
		<TheSidebarAdmin></TheSidebarAdmin>
		<div class="content-container">
			<TheHeaderAdmin></TheHeaderAdmin>
			<TheBodyAdmin></TheBodyAdmin>
		</div>
	</div>
</template>
<style scoped lang="scss">
.admin-page {
	min-height: 100vh;
	width: auto;
	display: flex;
	padding: 16px 24px;
	background-color: var(--color-background);
	gap: 32px;
}
.content-container {
	width: 100%;
	background-color: inherit;
}
</style>
