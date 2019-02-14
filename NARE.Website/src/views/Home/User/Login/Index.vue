<template>
	<FormNarrowCard title="Login" :submit="submit" v-if="this.$route.name === 'login'">
		<div slot="card-information">
			<p v-if="redirect" class="text-danger text-center mb-3">You must be logged in to view this. Please login below.</p>
            <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
		</div>

		<div slot="card-content" class="text-center">
			<FormEmail v-model="email" :validator="$v.email"/>
			<FormPassword v-model="password" :validator="$v.password"/>
			
			<div class="mb-3">
				<router-link :to="{ name: 'resetPassword' }">Forgot your password?</router-link>
			</div>

			<div class="mb-3">
				<router-link :to="{ name: 'register' }">Not registered? Register here</router-link>
			</div>

			<button class="btn btn-lg btn-secondary btn-block text-uppercase" type="submit">Login</button>
		</div>
	</FormNarrowCard>
	<div v-else>
		<router-view></router-view>
	</div>
</template>

<script>
import FormNarrowCard from '@/components/UI/Card/Form/FormNarrowCard.vue'
import FormEmail from "@/components/UI/Form/Email.vue"
import FormPassword from "@/components/UI/Form/Password.vue"

import { required, minLength, email } from "vuelidate/lib/validators"

export default {
	name: "LoginIndex",
	components: {
		FormNarrowCard,
		FormEmail,
		FormPassword,
	},
	data() {
		return {
			email: "",
			password: "",
			rememberMe: true,
			redirect: this.$route.params.redirect,
			error: false,
			errorMessage: "Failed to login. Please try again"
		};
	},
	validations: {
		email: {
			required,
			email
		},
		password: {
			required,
			minLength: minLength(6)
		}
	},
	methods: {
		submit() {
			this.$v.$touch();
			if (this.$v.$invalid) {
				return;
			}
			this.$store
				.dispatch("authentication/login", {
					email: this.email,
					password: this.password,
					rememberMe: this.rememberMe
				})
				.then(() => {
					this.error = false
					this.$router.push({ name: "home" })
				})
				.catch((error) => {
					if (error.response) {
						if (String(error.response.data.error[0]).toLowerCase().includes("email not confirmed")) {
							this.$router.push({ name: "confirmEmail" })
						}
						this.errorMessage = error.response.data.error[0]
					}
					this.error = true
				})
		}
	}
}
</script>