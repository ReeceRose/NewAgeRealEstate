<template>
    <div class="row">
        <div v-for="(listing, index) in listings" :key="index" class="col-lg-4 col-md-6 col-sm-12">
            <Listing :listing="listing"/>
        </div>
    </div>
</template>

<script>
import Listing from '@/components/UI/Card/Listing.vue'

export default {
    name: 'Listings',
    components: {
        Listing
    },
    props: {
        listings: Array
    },
}
</script>
