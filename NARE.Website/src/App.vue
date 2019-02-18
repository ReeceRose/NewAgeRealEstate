<template>
    <div id="App">
        <Spinner v-if="loading"/>
        <TheHeader/>
        <div>
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
@import "@/assets/scss/global.scss";
* {
    padding: 0;
    margin: 0;
    font-family: 'Montserrat', sans-serif;
}
h1 {
    padding-top: 75px;
    text-align: center;
}
.parallax {
    background-attachment: fixed;
    background-size: cover;
    background-repeat: no-repeat;
    width: 100%;
    max-width: 100%;
    opacity: 0.95;
}
.bg-blue {
    background-color: color(primaryBlue);
}
.font-main {
    color: color(mainTextHover);
}
.font-italic {
    font-style: italic;
}
.font-bold {
    font-weight: 800;
}
.fade-on-hover {
    &:hover {
        opacity: 0.75;
    }
}
</style>
