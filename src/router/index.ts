import { createRouter, createWebHistory } from "vue-router";
import LoginPageVue from "@/views/auth/LoginPage.vue";
const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: "/login",
			name: "LoginPage",
			component: LoginPageVue,
		},
	],
});

export default router;
