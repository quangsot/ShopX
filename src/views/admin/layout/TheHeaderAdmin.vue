<script setup lang="ts">
import { ref } from "vue";
</script>
<template>
	<div class="header-admin">
		<div class="user">
			<p>Chào, <span>Quang</span></p>
			<p class="role">Admin</p>
		</div>
		<div class="avatar">
			<img
				src="../../../assets/logo/logo.jpg"
				alt="avatar-user"
			/>
		</div>
	</div>
</template>
<style scoped lang="scss">
.header-admin {
	width: 100%;
	height: 56px;
	background-color: inherit;
	display: flex;
	align-items: center;
	justify-content: end;
	gap: 24px;
	.user {
		.role {
			text-align: end;
		}
		p {
			span {
				font-weight: 800;
			}
		}
	}
	.avatar {
		user-select: none;
		width: 50px;
		height: 50px;
		border: none;
		border-radius: 100%;
		background-color: aqua;
		overflow: hidden;
		img {
			width: 100%;
			height: 100%;
			object-fit: cover;
		}
	}
}
</style>
