/**
 * Hàm bắt sự kiện click outside
 */
const clickOutSide = {
	beforeMount: (el: any, binding: any) => {
		el.clickOutSideEvent = (event: Event) => {
			if (!(el == event.target || el.contains(event.target))) {
				binding.value(event);
				// binding.value(event, event.target);
			}
		};
		document.addEventListener("click", el.clickOutSideEvent, true);
	},
	unmounted: (el: any) => {
		document.removeEventListener("click", el.clickOutSideEvent, true);
	},
};
export default clickOutSide;
