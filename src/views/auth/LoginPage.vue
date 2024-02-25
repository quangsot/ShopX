<script setup lang="ts"></script>
<template>
	<div class="body-login">
		<div class="form-container">
			<div class="head">
				<img
					src="../../assets/logo/logo.jpg"
					alt="logo-shop-x"
				/>
			</div>
			<div class="main">
				<div class="username input">
					<label for="username">Tên đăng nhập</label>
					<font-awesome-icon
						class="icon"
						:icon="['fas', 'user']"
					/>
					<input
						type="text"
						id="username"
						placeholder="Nhập email của bạn"
					/>
				</div>
				<div class="password input">
					<label for="password">Mật khẩu</label>
					<font-awesome-icon
						class="icon"
						:icon="['fas', 'lock']"
					/>
					<input
						tabindex="1"
						type="password"
						id="password"
						placeholder="Nhập mật khẩu của bạn"
					/>
				</div>
				<div
					tabindex="2"
					class="submit-btn"
				>
					Đăng nhập
				</div>
			</div>
			<div class="foot">
				<div class="fogot-password"><span>Quên mật khẩu ?</span></div>
				<div class="title-orther-login">Đăng nhập bằng</div>
				<div class="orther-login">
					<div class="facebook-login">
						<font-awesome-icon :icon="['fab', 'facebook-f']" />
					</div>
					<div class="google-login">
						<font-awesome-icon :icon="['fab', 'google']" />
					</div>
				</div>
				<div class="register">
					<div>
						Chưa có tài khoản? <span>Đăng kí ngay <font-awesome-icon :icon="['fas', 'arrow-right']" /></span>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>
<style scoped lang="scss">
//@import "../../assets/scss/global.scss";
.body-login {
	background-image: linear-gradient(to bottom left, var(--color-main) 50%, #ffffff 50%);
	width: 100vw;
	height: 100vh;
	display: flex;
	align-items: center;
	justify-content: center;
	.form-container {
		width: 500px;
		height: 600px;
		// background-color: #ffffff65;
		background-image: linear-gradient(to top right, var(--color-main) 20%, #ffffff 20%);
		border: none;
		border-radius: 10px;
		overflow: hidden;
		cursor: default;
		.head {
			width: 100%;
			height: 150px;
			background-color: #ffffff;
			display: flex;
			align-items: center;
			justify-content: center;
		}

		.main {
			width: 100%;
			height: 250px;
			background-color: inherit;
			padding: 0px 20px;
			display: flex;
			// justify-content: space-between;
			flex-direction: column;
			gap: 50px;
			label {
				color: var(--color-text);
				font-size: 18px;
				margin-bottom: 8px;
				cursor: pointer;
			}
			.input {
				position: relative;
				width: 100%;
				height: 36px;
				input {
					background-color: #ffffff65;
					width: 100%;
					height: 100%;
					border: none;
					border-bottom: solid 3px var(--color-grey-400);
					outline: none;
					padding-left: 50px;
					font-size: 16px;
					font-weight: 600;
					&::placeholder {
						color: var(--color-text-muted);
						font-size: small;
						font-weight: 300;
					}
				}
				.icon {
					position: absolute;
					top: 100%;
					left: 10px;
				}
			}
			.submit-btn {
				width: 390px;
				height: 50px;
				border: none;
				border-radius: 50px;
				background-color: var(--color-main);
				margin: 0 auto;
				margin-top: 20px;
				display: flex;
				align-items: center;
				justify-content: center;
				color: #ffffff;
				outline: none;
				cursor: pointer;
				&:hover {
					background-color: var(--color-blue-400);
				}
				&:active {
					background-color: #ffffff;
					color: var(--color-main);
					border: solid 1px var(--color-main);
				}
			}
		}

		.foot {
			background-color: inherit;
			.fogot-password {
				span {
					cursor: pointer;
					color: var(--color-main);
					&:hover {
						text-decoration: underline;
					}
				}
				background-color: inherit;
				padding-right: 20px;
				display: flex;
				justify-content: end;
			}
			.title-orther-login {
				text-align: center;
				margin-bottom: 10px;
			}
			.orther-login {
				display: flex;
				align-items: center;
				justify-content: center;
				gap: 20px;
				margin-bottom: 40px;
				svg {
					width: 30px;
					height: 30px;
					cursor: pointer;
					color: #ffffff;
				}
				.facebook-login {
					width: 45px;
					height: 45px;
					border-radius: 100%;
					display: flex;
					align-items: center;
					justify-content: center;
					background-color: blue;
				}
				.google-login {
					width: 45px;
					height: 45px;
					border-radius: 100%;
					display: flex;
					align-items: center;
					justify-content: center;
					background-color: red;
				}
			}
			.register {
				display: flex;
				align-items: center;
				justify-content: end;
				padding-right: 20px;
				div {
					width: fit-content;
					span {
						color: var(--color-main);
						text-decoration: underline;
						cursor: pointer;
					}
				}
			}
		}
	}
}
</style>
