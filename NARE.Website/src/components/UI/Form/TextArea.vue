<template>
    <div class="form-label-group">
        <textarea
            v-model="input"
            @blur="test()"
            :class="{ 'is-invalid': validator.$error }"
            type="text" 
            :id="id"
            class="form-control" 
            :placeholder="placeholder" 
            autofocus
        >
        </textarea>
        <p v-if="validator.$error" class="text-danger text-center">{{ errorMessage }}</p>
    </div>
</template>

<script>
export default {
    name: 'TextAreaInput',
    props: {
        value: {
            required: false,
            default: ""
        },
        validator: {
            type: Object,
            required: true  
        },
        errorMessage: {
            type: String,
            required: false,
            default: "Invalid input"
        },
        placeholder: {
            type: String,
            required: true
        },
        id: {
            type: String,
            required: true
        }
    },
    computed: {
        input: {
            get() {
                return this.value
            },
            set(value) {
                this.$emit("input", value)
            }
        }
    },
    methods: {
        test() {
            this.validator.$touch()
        }
    }
}
</script>