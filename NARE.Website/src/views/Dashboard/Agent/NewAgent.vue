<template>
    <FormNarrowCard title="New Agent" :submit="submit">
        <div slot="card-information">
            <p v-if="success" class="text-success text-center mb-3">User successfully registered.</p>
            <p v-if="error" class="text-danger text-center mb-3">{{ errorMessage }}</p>
        </div>

        <div slot="card-content">
            <TextInput id="nameInput" v-model="name" :validator="$v.name" errorMessage="Invalid agent name" placeholder="Agent name"/>
            <FormEmail v-model="email" :validator="$v.email"/>
            <FormPassword v-model="password" :validator="$v.password"/>
            <FormPassword v-model="confirmationPassword" confirmationPassword="true" :validator="$v.confirmationPassword"/>

            <button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="$router.go(-1)">Return <i class="fas fa-undo"></i></button>
            <button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" type="submit">Create</button>
        </div>
    </FormNarrowCard>
</template>

<script>
import TextInput from '@/components/UI/Form/Text.vue'
import FormNarrowCard from '@/components/UI/Card/Form/FormNarrowCard.vue'
import FormEmail from '@/components/UI/Form/Email.vue'
import FormPassword from '@/components/UI/Form/Password.vue'

import { required, minLength, email, sameAs, helpers } from 'vuelidate/lib/validators'
const passwordRegex = helpers.regex('passwordRegex', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)

export default {
    name: 'newAgent',
    components: {
        TextInput,
        FormNarrowCard,
        FormEmail,
        FormPassword
    },
    data() {
        return {
            name: '',
            email: '',
            password: '',
            confirmationPassword: '',
            success: null,
            error: null,
            errorMessage: 'An error has occured, make sure your passwords match and your email is unique'
        }
    },
    validations: {
        name: {
            required
        },
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
            this.$store.dispatch('authentication/create', { name: this.name, email: this.email, password: this.password })
                .then(() => {
                    this.error = null
                    this.success = true
                })
                .catch(() => {
                    this.error = true
                })
        },
    }
}
</script>