<template>
    <div id="App">
        <Spinner v-if="loading"/>
        <TheHeader/>
        <div class="container">
            <router-view/>
        </div>
        <TheFooter/>
    </div>
</template>

<script>
import Spinner from '@/components/UI/Spinner.vue'
import TheHeader from '@/components/UI/Navigation/Navigation.vue'
import TheFooter from '@/components/UI/Navigation/Footer.vue'

export default {
    name: 'App',
    components: {
        Spinner,
        TheHeader,
        TheFooter
    },
    computed: {
        loading() {
            return this.$store.getters['global/isLoading']
        }
    }
}
</script>


<style lang="scss">
* {
    padding: 0;
    margin: 0;
    font-family: 'Montserrat', sans-serif;
}
h1, .form {
    padding-top: 75px;
    text-align: center;
}
</style>
