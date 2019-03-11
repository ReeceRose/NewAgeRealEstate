<template>
    <div class="container pt-5 pb-5">
        <a @click="$router.go(-1)" class="btn btn-link"><i class="fas fa-arrow-left"></i> Back</a>
        <div class="row pt-3">
            <div class="col-lg-9 col-md-8 col-sm-12 pb-3">
                <div class="property border-main">
                    <img class="img-fluid" :src="post.imageUrl" alt="Main picture">
                    <div class="row p-3">
                        <div v-for="(picture, index) in post.pictures" :key="index" class="col-lg-3 col-md-4 col-sm-6 pb-4">
                            <img :src="picture.src" :alt="picture.title" class="img-fluid rounded" @click="toggleModal(index)">
                        </div>
                        <div class="modal" v-if="displayModal">
                            <div class="modal-header">
                                <button class="close text-white" @click="displayModal = false">&times;</button>
                            </div>
                            <div class="modal-body text-center">
                                <div class="carousel slide" data-interval="false" data-ride="carousel">
                                    <div class="carousel-inner">
                                        <div class="carousel-item" v-for="(picture, pictureIndex) in post.pictures" :key="pictureIndex" :class="pictureIndex == currentImage ? 'active' : ''">                            
                                            <img :src="picture.src" :alt="picture.title" class="image-modal">
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
                    <p class="text-center address font-bold mb-2">{{ post.address }}</p>
                    <p class="text-center address-minor">{{ post.city }}, {{ post.province }}, {{ post.postalCode }}</p>
                    <hr>
                    <div class="row details pl-5 pr-5">
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-money-bill"></i> Asking Price:</p></div>
                                <div class="col text-right">$ {{ post.price }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-bed"></i> Bedrooms:</p></div>
                                <div class="col text-right">{{ post.bedrooms }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-bath"></i> Bathrooms:</p></div>
                                <div class="col text-right">{{ post.bathrooms }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-car"></i> Garage:</p></div>
                                <div class="col text-right">{{ post.garage }}</div>
                            </div>
                            <hr>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-square"></i> Square Feet:</p></div>
                                <div class="col text-right">{{ post.squareFootage }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-square"></i> Lot Size:</p></div>
                                <div class="col text-right">{{ post.lotSize }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-calendar-alt"></i> Listing Date:</p></div>
                                <div class="col text-right">{{ post.listingDate }}</div>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col"><p><i class="fas fa-home"></i> Year Built:</p></div>
                                <div class="col text-right">{{ post.yearBuilt }}</div>
                            </div>
                            <hr>
                        </div>
                    </div>
                    <hr>
                    <div class="row">
                        <div class="col p-5">
                            {{ post.description }}
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-12 pb-3">
                <div class="agent border-main text-center">
                    <img class="img-fluid" :src="post.agent.picture" :alt="post.agent.name">
                    <p class="agent-name"><router-link :to="{ name: 'agent', params: { id: post.agent.id } }"><i class="fas fa-user"></i> {{ post.agent.name }}</router-link></p>
                    <p>Inquire here</p>
                    <hr>
                    <p><a :href="`tel:${post.agent.telephone}`"><i class="fas fa-phone"></i> {{ post.agent.telephone }}</a></p>
                    <p><a :href="`mailto:${post.agent.email}`"><i class="fas fa-envelope"></i> {{ post.agent.email }}</a></p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'listing',
    methods: {
        toggleModal(imageIndex) {
            this.displayModal = !this.displayModal
            this.currentImage = imageIndex
        },
        nextSlide() {
            if (this.currentImage >= this.post.pictures.length - 1) {
                this.currentImage = 0
            } else {
                this.currentImage++
            }
        },
        previouseSlide() {
            if (this.currentImage <= 0) {
                this.currentImage = this.post.pictures.length - 1
            } else {
                this.currentImage--
            }
        }
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
    data() {
        return {
            displayModal: false,
            currentImage: 0,
            post: {
                    id: '1231',
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    lotSize: '1.2 Acres',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '1115000',
                    yearBuilt: '2009',
                    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi dapibus scelerisque congue. Nullam aliquet efficitur rutrum. Praesent ac justo dictum, vehicula quam sed, vestibulum nunc. Curabitur fringilla tempor lacus sit amet pellentesque. Ut elit sem, scelerisque ac lectus at, congue bibendum arcu. Curabitur scelerisque non diam et volutpat. Aenean finibus egestas sapien, in vehicula est maximus pretium. Sed id nisi orci. Vivamus ut commodo lacus. Proin a arcu vel nulla fermentum pretium. ',
                    agent: {
                        id: '456',
                        name: 'Jane Doe',
                        picture: 'https://images.pexels.com/photos/733872/pexels-photo-733872.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                        telephone: '111-222-3333',
                        email: 'nare@reecerose.com'
                    },
                    pictures: [
                        {
                            src: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940',
                            title: 'Alt'
                        },
                        {
                            src: 'https://images.pexels.com/photos/584399/living-room-couch-interior-room-584399.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                            title: 'Alt'
                        },
                        {
                            src: 'https://images.pexels.com/photos/276554/pexels-photo-276554.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                            title: 'Alt'
                        },
                        {
                            src: 'https://images.pexels.com/photos/276554/pexels-photo-276554.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                            title: 'Alt'
                        },
                        {
                            src: 'https://images.pexels.com/photos/276554/pexels-photo-276554.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                            title: 'Alt'
                        },
                        {
                            src: 'https://images.pexels.com/photos/276554/pexels-photo-276554.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                            title: 'Alt'
                        },
                    ],
                    listingDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                }
        }
    }
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