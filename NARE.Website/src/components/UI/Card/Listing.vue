<template>
    <div class="pb-4">
        <img class="img-fluid w-100" :src="post.imageUrl" :alt="post.address" :title="post.address">
        <div class="price">${{post.price}}</div>
        <div class="information border-main">
            <div class="address text-center">
                <p class="address-main font-bold">{{ post.address }}</p>
                <p class="address-minor"><i class="fas fa-map-marker-alt"></i> {{ post.city }}, {{ post.province }}, {{ post.postalCode }}</p>
            </div>
            <div class="details pt-0">
                <hr>
                <div class="row">
                    <div class="col"><i class="fas fa-square"></i>Sqft: {{ post.squareFootage }}</div>
                    <div class="col"><i class="fas fa-car"></i>Garage: {{ post.garage }}</div>
                </div>
                <div class="row">
                    <div class="col"><i class="fas fa-bed"></i>Bedrooms: {{ post.bedrooms }}</div>
                    <div class="col"><i class="fas fa-bath"></i>Bathrooms: {{ post.bathrooms }}</div>
                </div>
                <hr>
                <div class="row pb-2">
                    <div class="col text-center">
                        <router-link :to="{ name: 'home' }"><i class="fas fa-user"></i> {{ post.agent }}</router-link>
                    </div>
                </div>
                <div class="row pb-2">
                    <div class="col text-center"><i class="fas fa-calendar-alt"></i>{{ post.listingDate }}</div>
                </div>
                <div class="row">
                    <div class="col">
                        <router-link :to="{ name: 'listing', params: { id: post.id } }" class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase">View</router-link>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Listing',
    props: {
        post: Object
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss"; 
.price {
    position: absolute;
    top: 0;
    background: color(primaryBlue);
    opacity: 0.75;
    color: #fff;
    text-align: left;
    padding: 1.2em;
}
.fas {
    color: color(primaryBlue);
}
.information {
    .address-main {
        font-size: 1.5rem;
        padding: 0.8rem 0.8rem 0 0.8rem;
    }
    
    .address-minor {
        font-size: 0.75rem;
    }

    .details {
        padding: 1.25rem;
        font-size: 0.95rem;
    }
}
</style>
