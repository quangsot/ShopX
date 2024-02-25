<script setup lang="ts">
import SidebarItemCpn from "@/components/menu/SidebarItemCpn.vue";
</script>
<template>
	<aside class="sidebar-admin">
		<div class="toggle">
			<div class="logo">
				<img
					src="../../../assets/logo/logo.jpg"
					alt="logo-shop-x"
				/>
			</div>
		</div>
		<div class="sidebar">
			<SidebarItemCpn
				text="Trang chủ"
				icon="fa-solid fa-house"
			/>
			<SidebarItemCpn
				text="Đơn hàng"
				icon="fa-solid fa-cart-shopping"
			/>
			<SidebarItemCpn
				text="Sản phẩm"
				icon="fa-solid fa-box-archive"
			/>
			<SidebarItemCpn
				text="Khách hàng"
				icon="fa-solid fa-users"
			/>
		</div>
		<div class="logout">
			<SidebarItemCpn
				text="Đăng xuất"
				icon="fa-solid fa-right-from-bracket"
			/>
		</div>
	</aside>
</template>
<style scoped lang="scss">
.sidebar-admin {
	height: auto;
	min-width: 200px;
	width: fit-content;
	background-color: var(--color-white);
	overflow: hidden;
	border-radius: var(--border-radius-1);
	position: relative;
	box-shadow: var(--box-shadow);
	transition: all 0.3s ease;
	&:hover {
		box-shadow: none;
	}
	.logout {
		width: 100%;
		position: absolute;
		bottom: 56px;
		left: 0;
		padding-left: 28px;
	}
}
.toggle {
	.logo {
		img {
			width: 200px;
			height: 80px;
			border-radius: var(--border-radius-1);
		}
	}
}
.sidebar {
	background-color: var(--color-white);
	padding-left: 28px;
}
</style>
