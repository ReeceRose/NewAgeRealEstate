<template>
    <FormNarrowCard title="Register" :submit="submit">
        <div slot="card-information">
            <p v-if="success" class="text-success text-center mb-3">A confirmation email has been sent.</p>
            <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
        </div>

        <div slot="card-content">
            <FormEmail v-model="email" :validator="$v.email"/>
            <FormPassword v-model="password" :validator="$v.password"/>
            <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>

            <button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Register</button>
        </div>
    </FormNarrowCard>
</template>

<script>
import FormNarrowCard from '@/components/UI/Card/Form/FormNarrowCard.vue'
import FormEmail from '@/components/UI/Form/Email.vue'
import FormPassword from '@/components/UI/Form/Password.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
    components: {
        FormNarrowCard,
        FormEmail,
        FormPassword
    },
    data() {
        return {
            email: '',
            password: '',
            confirmationPassword: '',
            success: null,
            error: null,
            errorMessage: 'An error has occured, make sure your passwords match and your email is unique'
        }
    },
    validations: {
        email: {
            required,
            email
        },
        password: {
            required,
            minLength: minLength(6),
            passwordRegex
        },
        confirmationPassword: {
            required,
            sameAsPassword: sameAs('password')
        }
    },
    methods: {
        submit() {
            this.$v.$touch()
            if (this.$v.$invalid) {
                return
            }
            this.$store.dispatch('authentication/register', { email: this.email, password: this.password })
                .then(() => {
                    this.error = null
                    this.success = true
                })
                .catch((error) => {
                    if (error.response) {
                        this.errorMessage = error.response.data.error[0]
                    }
                    this.error = true
                })
        }
    }
}
</script>

<style lang="scss" scoped>

</style>