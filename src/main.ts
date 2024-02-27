import "./assets/scss/main.scss";
import "@/assets/scss/global.scss";
import { createApp } from "vue";
import { createPinia } from "pinia";

import App from "./App.vue";
import router from "./router";

// Import tất cả các icon từ thư viện Font Awesome
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { far } from "@fortawesome/free-regular-svg-icons";
import { fab } from "@fortawesome/free-brands-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
// Thêm các icon vào thư viện
library.add(fas, far, fab);

// cpn chung
import InputCpn from "@/components/input/text-field/InputCpn.vue";
import ButtonCpn from "./components/button/ButtonCpn.vue";
import IconCpn from "@/components/icon/IconCpn.vue";
import ComboboxCpn from "@/components/input/ComboboxCpn.vue";

// directive
import tooltip from "./helper/directives/TooltipDirective.js";
import clickOutSide from "./helper/directives/OutSideDirective";
const app = createApp(App);

app.use(createPinia());
app.use(router);
app.component("font-awesome-icon", FontAwesomeIcon);
app.component("InputCpn", InputCpn).component("ButtonCpn", ButtonCpn).component("IconCpn", IconCpn).component("ComboboxCpn", ComboboxCpn);
app.directive("tooltip", tooltip).directive("outside", clickOutSide);
app.mount("#app");
