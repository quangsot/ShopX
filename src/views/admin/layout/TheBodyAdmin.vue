<script setup lang="ts"></script>
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
					/>
					<IconCpn
						class="reload"
						icon="fa-solid fa-rotate"
					/>
					<IconCpn
						class="export-excel-feature"
						icon="fa-solid fa-right-to-bracket"
					/>
				</div>
			</div>
			<div class="table-wrapper">
				<div class="table-container">
					<table class="main-content">
						<thead>
							<tr>
								<th>STT</th>
								<th>Tên sản phẩm</th>
								<th>Hãng</th>
								<th>Nhà cung cấp</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="num in [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]">
								<td>{{ num }}</td>
								<td>Áo thun {{ num }}</td>
								<td>Hãng {{ num }}</td>
								<td>Nhà cung cấp {{ num }}</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div class="table-footer">
					<div class="total-record">Tổng số: <span>100</span></div>
					<div class="paging">
						<div class="record-page-number">
							<p>Bản ghi/Trang: 10</p>
							<div>Combobox</div>
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
	height: auto;
	padding: 12px 0;
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
				.search-feature {
					box-shadow: var(--box-shadow);
					transition: all 0.4s ease;
					&:hover {
						box-shadow: none;
					}
				}
			}
		}
		.table-wrapper {
			padding-left: 10px;
			background-color: var(--color-white);
			box-shadow: var(--box-shadow);
			border-radius: var(--border-radius-2);
			transition: all 0.4s ease;
			&:hover {
				box-shadow: none;
			}
			.table-container {
				height: 450px;
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
			}
			.table-footer {
				display: flex;
				align-items: center;
				justify-content: space-between;
				padding-right: 10px;
				height: 60px;
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
										width: 18px;
										height: 18px;
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
			border-bottom: solid 1px transparent;
		}
		&:active {
			background-color: transparent;
		}
		td {
			min-width: 100px;
			height: 44px;
			cursor: pointer;
			text-align: center;
			color: var(--color-dark-variant);

			&:first-child {
				border-top-left-radius: var(--border-radius-1);
				border-bottom-left-radius: var(--border-radius-1);
			}
			&:last-child {
				border-top-right-radius: var(--border-radius-1);
				border-bottom-right-radius: var(--border-radius-1);
			}
		}
	}
}
</style>
