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
                        <h2 class="font-main">Search Listings</h2>
                        <SearchListings/>
                    </div>
                </div>
            </div>
            <div class="row my-3">
                <div class="col">
                    <div class="featured-listings">
                        <h2 class="font-main">Featured Listings</h2>
                        <!-- TODO: Pass the featured listings -->
                        <Listings :posts="null"/>
                    </div>
                </div>
            </div>
            <div class="row my-3">
                <div class="col">
                    <div class="new-listings">
                        <h2 class="font-main">New Listings</h2>
                        <!-- TODO: Pass the 3 most recent listings -->
                        <Listings :posts="null"/>
                        <router-link :to="{ name: 'listings' }" class="mt-3 btn btn-main bg-blue font-main fade-on-hover text-uppercase">View All</router-link>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import SearchListings from '@/components/UI/Home/SearchListings.vue'
import Listings from '@/components/UI/Home/Listings.vue'

export default {
    name: 'home',
    components: {
        SearchListings,
        Listings
    }
}
</script>