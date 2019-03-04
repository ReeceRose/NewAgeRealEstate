<template>
    <div class="container p-5">
        <Listings :listings="null"/>
    </div>
</template>

<script>
import Listings from '@/components/UI/Home/Listings.vue'

export default {
    name: 'listings',
    components: {
        Listings
    }
}
</script>
