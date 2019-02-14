<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-secondary">
        <div class="container">
            <router-link :to="{ name: 'home' }" class="navbar-brand">New Age Real Estate</router-link>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navigationBar" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse text-center" id="navigationBar">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <router-link :to="{ name: 'home' }" class="btn-header">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn-header">About</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn-header">Featured</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link :to="{ name: 'about' }" class="btn-header">Listings</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item" v-if="!loggedIn">
                        <router-link :to="{ name: 'login' }" class="btn-header"><i class="fas fa-sign-in-alt"></i> Login</router-link>
                    </li>
                    <li class="nav-item" v-else>
                        <span class="btn-header" @click="logout"><i class="fas fa-sign-out-alt"></i> Logout</span>
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
.btn-header {
    padding: 10px;
    color: #fff;
    text-decoration: none;

    &:hover {
        color: #b4b6ba;
    }
}
.nav-item {
    padding: 5px;
}
</style>
