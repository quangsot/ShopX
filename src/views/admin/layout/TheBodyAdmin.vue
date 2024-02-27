<script setup lang="ts">
import CheckboxCpn from "@/components/input/CheckboxCpn.vue";

import { ref } from "vue";

const isActiveRow = ref<boolean>(false);

const activeRow = ref<string[]>([]);

const pageNumber = ref<string>();
</script>
<template>
	<div class="body-admin">
		<div class="title">
			<p>Sản phẩm</p>
			<ButtonCpn
				style="width: 180px"
				content="Thêm sản phẩm"
				icon="fa-solid fa-plus"
			></ButtonCpn>
		</div>
		<div class="main-container">
			<div class="feature-table">
				<div class="table-feature">
					<InputCpn
						class="search-feature"
						type="text"
						trailingIcon="fa-solid fa-magnifying-glass"
						placeholder="nhập tên hoặc SKU"
						:validate="false"
					/>
					<div v-tooltip="'Làm mới'">
						<IconCpn
							class="reload"
							icon="fa-solid fa-rotate"
						/>
					</div>
					<div v-tooltip="{ text: 'Xuất Excel', theme: { placement: 'left' } }">
						<IconCpn
							class="export-excel-feature"
							icon="fa-solid fa-right-to-bracket"
						/>
					</div>
				</div>
			</div>
			<div class="table-wrapper">
				<div class="table-container">
					<table class="main-content">
						<thead>
							<tr>
								<th>
									<CheckboxCpn />
								</th>
								<th>STT</th>
								<th>Tên sản phẩm</th>
								<th>Hãng</th>
								<th>Nhà cung cấp</th>
								<th></th>
							</tr>
						</thead>
						<tbody>
							<tr
								v-for="num in [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]"
								:key="num"
								:class="{ 'active-row': activeRow.includes(`${num}`) }"
							>
								<!-- :class="{ 'active-row': checkList[num] }" -->
								<td>
									<CheckboxCpn
										:value="`${num}`"
										@checked="
											(value) => {
												if (value[0]) {
													activeRow.push(value[1]);
												} else activeRow = activeRow.filter((item) => item != value[1]);
												console.log('active item>>>', activeRow);
											}
										"
									/>
								</td>
								<td>{{ num }}</td>
								<td>Áo thun {{ num }}</td>
								<td>Hãng {{ num }}</td>
								<td>Nhà cung cấp {{ num }}</td>
								<td class="context-menu">
									<div class="feature-row">
										<div
											class="edit-feature"
											v-tooltip="'Sửa'"
										>
											<IconCpn
												class="icon-feature-row"
												icon="fa-regular fa-pen-to-square"
											/>
										</div>
										<div
											class="delete-feature"
											v-tooltip="'Xóa'"
										>
											<IconCpn
												class="icon-feature-row"
												icon="fa-regular fa-trash-can"
											/>
										</div>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div class="table-footer">
					<div class="total-record">Tổng số: <span>100</span></div>
					<div class="paging">
						<div class="record-page-number">
							<p>Bản ghi/Trang: {{ pageNumber }}</p>
							<div>
								<ComboboxCpn
									style="width: 80px"
									:validate="false"
									:values="[10, 20, 50, 100]"
									:displays="['10', '20', '50', '100']"
									:autoSelect="true"
									:readonly="true"
									trailing-icon="fa-solid fa-angle-up"
									menuPosition="top"
									@getItemSelected="(value: [string,string])=>pageNumber = value[1]"
								/>
							</div>
						</div>
						<div class="index-page">
							<p>1 - 10 bản ghi</p>
							<div class="arrow">
								<div class="angle left">
									<IconCpn icon="fa-solid fa-angle-left" />
								</div>
								<div class="angle right">
									<IconCpn icon="fa-solid fa-angle-right" />
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>
<style scoped lang="scss">
.body-admin {
	width: 100%;
	height: calc(100% - 56px);
	padding-top: 12px;
	//background-color: inherit;
	.title {
		margin-bottom: 24px;
		display: flex;
		justify-content: space-between;
		p {
			font-size: 26px;
			font-weight: 700;
		}
	}
	.main-container {
		height: calc(100% - 80px);
		display: flex;
		flex-direction: column;
		.feature-table {
			margin-bottom: 12px;
			.table-feature {
				display: flex;
				align-items: center;
				justify-content: end;
				gap: 16px;
				.export-excel-feature,
				.reload {
					width: 20px;
					height: 20px;
					cursor: pointer;
				}
			}
		}
		.table-wrapper {
			flex-grow: 1;
			padding: 10px 10px 0;
			background-color: var(--color-white);
			box-shadow: var(--box-shadow);
			border-radius: var(--border-radius-2);
			transition: all 0.4s ease;
			&:hover {
				box-shadow: none;
			}
			.table-container {
				height: calc(100% - 60px);
				overflow: auto;
				&::-webkit-scrollbar {
					background-color: transparent;
					width: 10px;
				}
				&:hover {
					&::-webkit-scrollbar {
						width: 10px;
					}
					&::-webkit-scrollbar-thumb {
						border-radius: var(--border-radius-1);
						background-color: var(--color-info-light);
					}
				}
				.main-content {
					tbody {
						tr {
							&:hover {
								.context-menu {
									.feature-row {
										display: flex;
									}
								}
							}
							.context-menu {
								position: relative;
								width: 1px;
								background-color: transparent;
								.feature-row {
									width: 100px;
									height: 44px;
									display: none;
									align-items: center;
									justify-content: center;
									gap: 8px;
									position: absolute;
									top: 0;
									left: -100px;
									background-color: inherit;
									.icon-feature-row {
										width: 20px;
										height: 20px;
									}
									.edit-feature,
									.delete-feature {
										background-color: var(--color-background);
										transition: all 0.2s ease;
										&:hover {
											background-color: var(--color-white);
										}
										&:active {
											transform: scale(0.9);
										}
										width: 40px;
										height: 40px;
										border: solid 1px var(--color-info-light);
										border-radius: 100%;
										display: flex;
										align-items: center;
										justify-content: center;
									}
								}
							}
						}
					}
				}
			}
			.table-footer {
				display: flex;
				align-items: center;
				justify-content: space-between;
				padding-right: 10px;
				height: 60px;
				border-top: solid 1px var(--color-info-light);
				.total-record {
					span {
						font-weight: 700;
					}
				}
				.paging {
					user-select: none;
					display: flex;
					align-items: center;
					justify-content: space-between;
					gap: 28px;
					.record-page-number,
					.index-page {
						display: flex;
						gap: 20px;
						align-items: center;
					}
					.index-page {
						.arrow {
							display: flex;
							gap: 16px;
							cursor: pointer;
							width: 56px;
							.angle {
								width: 20px;
								height: 20px;
								svg {
									transition: all 0.05s ease;
									width: 20px;
									height: 20px;
									&:active {
										transform: scale(0.8);
									}
								}
								&:hover {
									color: var(--color-blue-300);
								}
							}
						}
					}
				}
			}
		}
	}
}
table {
	border-collapse: collapse;
	background-color: var(--color-white);
	width: 100%;
	position: relative;
	thead {
		tr {
			th {
				user-select: none;
				font-size: medium;
				font-weight: 600;
				color: var(--color-dark);
				height: 48px;
				position: sticky;
				top: 0;
				left: 0;
				background-color: var(--color-white);
				&:first-child {
					width: 50px !important;
				}
			}
		}
	}
	tr {
		height: 44px;
		width: fit-content;
		border: none;
		border-bottom: solid 1px var(--color-light);
		transition: all 0.3s ease;
		&:hover {
			background-color: var(--color-info-light);
			// border-bottom: solid 1px transparent;
		}
		&:active {
			background-color: transparent;
		}
		td {
			height: 44px;
			cursor: pointer;
			text-align: center;
			color: var(--color-dark-variant);

			&:first-child {
				border-top-left-radius: var(--border-radius-1);
				border-bottom-left-radius: var(--border-radius-1);
				width: 50px !important;
			}
			&:last-child {
				border-top-right-radius: var(--border-radius-1);
				border-bottom-right-radius: var(--border-radius-1);
			}
		}
	}
}
.active-row {
	background-color: var(--color-blue-100) !important;
}
</style>
