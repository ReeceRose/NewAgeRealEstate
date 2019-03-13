<template>
    <div class="container pt-5 pb-5">
        <a @click="$router.go(-1)" class="btn btn-link"><i class="fas fa-arrow-left"></i> Back</a>
        <div class="row pt-3" v-if="listing.address">
            <div class="col-lg-9 col-md-8 col-sm-12 pb-3">
                <div class="property border-main">
                    <img class="img-fluid" :src="listing.mainImageUrl" alt="Main picture">
                    <div class="row p-3">
                        <div v-for="(image, index) in listing.images" :key="index" class="col-lg-3 col-md-4 col-sm-6 pb-4">
                            <img :src="image.url" :alt="image.alternative" class="img-fluid rounded" @click="toggleModal(index)">
                        </div>
                        <div class="modal" v-if="displayModal">
                            <div class="modal-header">
                                <button class="close text-white" @click="displayModal = false">&times;</button>
                            </div>
                            <div class="modal-body text-center">
                                <div class="carousel slide" data-interval="false" data-ride="carousel">
                                    <div class="carousel-inner">
                                        <div class="carousel-item" v-for="(image, imageIndex) in listing.images" :key="imageIndex" :class="imageIndex == currentImage ? 'active' : ''">                            
                                            <img :src="image.url" :alt="image.alternative" class="image-modal">
                                        </div>
                                    </div>
                                    <a class="carousel-control-prev" @click="previouseSlide">
                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                    <a class="carousel-control-next" @click="nextSlide">
                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="text-center address font-bold mb-2">{{ listing.address }}</p>
                    <p class="text-center address-minor">{{ listing.city }}, {{ listing.provinceCode }}, {{ listing.postalCode }}</p>
                    <hr>
                    <div class="row details pl-5 pr-5">
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-money-bill"></i> Asking Price:</p></div>
                                <div class="col text-right">{{ format(listing.askingPrice) }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-bed"></i> Bedrooms:</p></div>
                                <div class="col text-right">{{ listing.bedroomCount }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-bath"></i> Bathrooms:</p></div>
                                <div class="col text-right">{{ listing.bathroomCount }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-car"></i> Garage:</p></div>
                                <div class="col text-right">{{ listing.garageSize }}</div>
                            </div>
                            <hr>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-square"></i> Square Feet:</p></div>
                                <div class="col text-right">{{ listing.squareFeet }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-square"></i> Lot Size:</p></div>
                                <div class="col text-right">{{ listing.lotSize }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-calendar-alt"></i> Listing Date:</p></div>
                                <div class="col text-right">{{ listing.listingDate.substr(0, 10) }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-home"></i> Year Built:</p></div>
                                <div class="col text-right">{{ listing.yearBuilt }}</div>
                            </div>
                            <hr>
                        </div>
                    </div>
                    <hr>
                    <div class="row">
                        <div class="col p-5">
                            {{ listing.description }}
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-12 pb-3">
                <div class="agent border-main text-center">
                    <img class="img-fluid" :src="listing.agentDto.imageUrl" :alt="listing.agentDto.name">
                    <p class="agent-name"><router-link :to="{ name: 'agent', params: { id: listing.agentDto.id } }"><i class="fas fa-user"></i> {{ listing.agentDto.name }}</router-link></p>
                    <p>Inquire here</p>
                    <hr>
                    <p><a :href="`tel:${listing.agentDto.telephone}`"><i class="fas fa-phone"></i> {{ listing.agentDto.phoneNumber }}</a></p>
                    <p><a :href="`mailto:${listing.agentDto.email}`"><i class="fas fa-envelope"></i> {{ listing.agentDto.email }}</a></p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'listing',
    data() {
        return {
            displayModal: false,
            currentImage: 0,
            listing: {}
        }
    },
    methods: {
        format(number) {
            return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
        },
        toggleModal(imageIndex) {
            this.displayModal = !this.displayModal
            this.currentImage = imageIndex
        },
        nextSlide() {
            if (this.currentImage >= this.listing.images.length - 1) {
                this.currentImage = 0
            } else {
                this.currentImage++
            }
        },
        previouseSlide() {
            if (this.currentImage <= 0) {
                this.currentImage = this.listing.images.length - 1
            } else {
                this.currentImage--
            }
        },
        getListing() {
            this.$store.dispatch("listings/listingById", this.$route.params.id)
                .then((result) => {
                    this.listing = result
                    this.pageCount = result.paginationModel.totalPages
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        }
    },
    created() {
        this.getListing()
    },
    mounted() {
        document.body.addEventListener('keyup', e => {
            if (e.keyCode === 27) {
                this.displayModal = false
            } else if (e.keyCode === 37) {
                this.previouseSlide();
            } else if (e.keyCode === 39) {
                this.nextSlide()
            }
        })
    },
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss";
.property, .details {
    font-size: 0.95rem;
}
.fas {
    color: color(primaryBlue);
}
.agent {
    .agent-name {
        font-size: 1.5rem;
        padding: 0.8rem 0.8rem 0 0.8rem;  
    }
}
.address {
    font-size: 1.5rem;
}
.address-minor {
    font-size: 0.75rem;
}
.modal {
    display: block;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: hidden;
    background-color: rgb(0,0,0);
    background-color: rgba(0,0,0,0.9);

    .modal-header {
        border: none;
    }

    .image-modal {
        width: 75vw;
        height: auto;
    }
}
</style>