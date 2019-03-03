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
                        <Listings :posts="null"/>
                    </div>
                </div>
            </div>
            <div class="row my-3">
                <div class="col">
                    <div class="new-listings">
                        <h2 class="font-main">New Listings</h2>
                        <Listings :posts="null"/>
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

<style lang="scss" scoped>
.parallax {
    background-image: url("https://static.reecerose.com/images/projects/NewAgeRealEstate/city-skyline.jpeg");
    height: 25vh;
}
</style>
