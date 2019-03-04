<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-blue">
        <div class="container">
            <router-link :to="{ name: 'home' }" class="navbar-brand fade-on-hover">New Age Real Estate</router-link>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse text-center" id="navigationBar">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <router-link :to="{ name: 'home' }" class="btn text-white fade-on-hover">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn text-white fade-on-hover">About</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'listings' }" class="btn text-white fade-on-hover">Listings</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item" v-if="!loggedIn">
                        <router-link :to="{ name: 'login' }" class="btn text-white fade-on-hover"><i class="fas text-white fa-sign-in-alt"></i> Login</router-link>
                    </li>
                    <li class="nav-item" v-else>
                        <span class="btn text-white fade-on-hover" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
export default {
    methods: {
        logout() {
            this.$store.dispatch("authentication/logout")
            this.$router.push({ name: 'home' })
        }
    },
    computed: {
        loggedIn() {
            return this.$store.getters['global/getToken']
        }
    }
}
</script>

<style lang="scss" scoped>
.nav-item {
    padding: 5px;
}
</style>
