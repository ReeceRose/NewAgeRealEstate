<template>
    <div class="pb-4">
        <img class="img-fluid w-100" :src="listing.mainImageUrl" :alt="listing.address" :title="listing.address">
        <div class="price">{{ format(listing.askingPrice) }}</div>
        <div class="information border-main">
            <div class="address text-center">
                <p class="address-main font-bold">{{ listing.address }}</p>
                <p class="address-minor"><i class="fas fa-map-marker-alt"></i> {{ listing.city }}, {{ listing.provinceCode }}, {{ listing.listingalCode }}</p>
            </div>
            <div class="details pt-0">
                <hr>
                <div class="row">
                    <div class="col"><i class="fas fa-square"></i> Sqft: {{ listing.squareFeet }}</div>
                    <div class="col"><i class="fas fa-car"></i> Garage: {{ listing.garageSize }}</div>
                </div>
                <div class="row">
                    <div class="col"><i class="fas fa-bed"></i> Bedrooms: {{ listing.bedroomCount }}</div>
                    <div class="col"><i class="fas fa-bath"></i> Bathrooms: {{ listing.bathroomCount }}</div>
                </div>
                <hr>
                <div class="row pb-2">
                    <div class="col text-center">
                        <router-link :to="{ name: 'agent', params: { id: listing.agent.id } }"><i class="fas fa-user"></i> {{ listing.agent.name }}</router-link>
                    </div>
                </div>
                <div class="row pb-2">
                    <div class="col text-center"><i class="fas fa-calendar-alt"></i> {{ listing.listingDate.substr(0, 10) }}</div>
                </div>
                <div class="row">
                    <div class="col">
                        <router-link :to="{ name: 'listing', params: { id: listing.id } }" class="btn btn-main btn-lg bg-blue fade-on-hover btn-block text-uppercase">View</router-link>
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
        listing: Object
    },
    methods: {
        format(number) {
            return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
        }
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
