<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-secondary">
        <div class="container">
            <router-link :to="{ name: 'home' }" class="navbar-brand">New Age Real Estate</router-link>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarToggler" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse text-center" id="navigationBar">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <router-link :to="{ name: 'home' }" class="btn btn-secondary">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn btn-secondary">About</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn btn-secondary">Featured</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn btn-secondary">Listings</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item" v-if="!loggedIn">
                        <router-link :to="{ name: 'login' }" class="btn btn-secondary"><i class="fas fa-sign-in-alt"></i> Login</router-link>
                    </li>
                    <li class="nav-item" v-else>
                        <span class="btn btn-secondary" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
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
.navbar-brand {
    color: #212226;
    font-weight: 700;
    &:hover {
        color: inherit;
        opacity: 0.75;
    }
}
.nav-item {
    padding: 5px;
}
</style>
