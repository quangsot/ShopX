import { createRouter, createWebHistory } from "vue-router";
import LoginPage from "@/views/auth/LoginPage.vue";
import AdminPage from "@/views/admin/AdminPage.vue";
const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: "/login",
			name: "LoginPage",
			component: LoginPage,
		},
		{
			path: "/admin",
			name: "AdminPage",
			component: AdminPage,
		},
	],
});

export default router;
