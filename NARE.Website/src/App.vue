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
    color: color(mainText);
}
.border-main {
    border: 1px solid color(border);
    border-top: none;
}
.btn-main {
    font-size: 0.75rem;
    border-radius: 5rem;
    letter-spacing: .1rem;
    font-weight: bold;
    padding: 1rem;
    color: color(mainText);
}
h1 {
    padding-top: 75px;
    text-align: center;
}
.parallax {
    background-image: url("https://static.reecerose.com/images/projects/NewAgeRealEstate/city-skyline.jpeg");
    height: 25vh;
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
    color: color(mainText);
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
.pointer {
    cursor: pointer;
}
</style>
