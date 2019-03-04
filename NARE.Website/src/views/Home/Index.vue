<template>
    <div>
        <div class="parallax">
            <h1 class="text-white font-bold">
                New Age Real Estate
            </h1>
        </div>
        <div class="container pb-5 text-center">
            <div class="row my-3">
                <div class="col">
                    <div class="search-listings">
                        <h2>Search Listings</h2>
                        <SearchListings/>
                    </div>
                </div>
            </div>
            <div class="row my-3">
                <div class="col">
                    <div class="featured-listings">
                        <h2>Featured Listings</h2>
                        <!-- TODO: Pass the featured listings -->
                        <p v-if="newListingsError" class="text-danger">Failed to load featured listings</p>
                        <Listings :listings="null" v-else/>
                    </div>
                </div>
            </div>
            <div class="row my-3">
                <div class="col">
                    <div class="new-listings">
                        <h2>New Listings</h2>
                        <p v-if="featuredListingsError" class="text-danger">Failed to load new listings</p>
                        <Listings :listings="null" v-else/>
                        <router-link :to="{ name: 'listings' }" class="mt-3 btn btn-main bg-blue fade-on-hover text-uppercase">View All</router-link>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import SearchListings from '@/components/UI/SearchListings.vue'
import Listings from '@/components/UI/Home/Listings.vue'

export default {
    name: 'home',
    components: {
        SearchListings,
        Listings
    },
    data() {
        return {
            featuredListings: null,
            featuredListingsError: false,
            newListings: null,
            newListingsError: false
        }
    },
    beforeCreate() {

    }
}
</script>