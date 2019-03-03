<template>
    <div>
        <img class="img-fluid w-100" :src="post.imageUrl" :alt="post.address" :title="post.address">
        <div class="price">${{post.price}}</div>
        <div class="information">
            <div class="address-main font-main font-bold">{{ post.address }}</div>
            <div class="address-minor font-main"><i class="fas fa-map-marker-alt"></i> {{ post.city }}, {{ post.province }}, {{ post.postalCode }}</div>
            <div class="details pt-0">
                <hr>
                <div class="row">
                    <div class="col">
                        <i class="fas fa-tape"></i>
                        Sqft: {{ post.squareFootage }}
                    </div>
                    <div class="col">
                        <i class="fas fa-car"></i>
                        Garage: {{ post.garage }}
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <i class="fas fa-bed"></i>
                        Bedrooms: {{ post.bedrooms }}
                    </div>
                    <div class="col">
                        <i class="fas fa-bath"></i>
                        Bathrooms: {{ post.bathrooms }}
                    </div>
                </div>
                <hr>
                <div class="row pb-2">
                    <div class="col">
                        <router-link :to="{ name: 'home' }"><i class="fas fa-user"></i> {{ post.agent }}</router-link>
                    </div>
                </div>
                <div class="row pb-2">
                    <div class="col">
                        <i class="fas fa-calendar-alt"></i>
                        {{ post.postedDate }}
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <!-- TODO: Change this to the listing -->
                        <router-link :to="{ name: 'home' }" class="btn btn-lg bg-blue font-main fade-on-hover btn-block text-uppercase">View</router-link>
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
.btn {
    font-size: 80%;
    border-radius: 5rem;
    letter-spacing: .1rem;
    font-weight: bold;
    padding: 1rem;
    transition: all 0.2s;
}
.price {
    position: absolute;
    top: 0;
    background: color(primaryBlue);
    opacity: 0.75;
    color: #fff;
    text-align: left;
    padding: 1.2em;

    @include mobile {
        padding: 0.1rem;
    }

    @include tablet {
        padding: 0.75rem;
    }

    @include desktop {
        padding: 1.2rem;
    }
}
.fas {
    color: color(primaryBlue);
}
.information {
    color: color(mainText);
    border: 1px solid rgb(158, 158, 158);
    border-top: none;

    .address-main {
        @include mobile {
            font-size: 1rem;
            padding: 0.1rem 0.1rem 0 0.1rem;
        }

        @include tablet {
            font-size: 1.2rem;
            padding: 0.75rem 0.75rem 0 0.75rem;
        }

        @include desktop {
            font-size: 1.5rem;
            padding: 0.8rem 0.8rem 0 0.8rem;
        }
    }
    
    .address-minor {
        color: color(mainText);
        font-size: 0.75rem;
    }

    .details {
        padding: 1.25rem;
        @include mobile {
            font-size: 0.55rem;
        }

        @include tablet {
            font-size: 0.65rem;
        }

        @include desktop {
            font-size: 0.85rem;
        }
    }
}
</style>
