<template>
    <div class="text-center p-5">
        <form @submit.prevent="submit">
            <div class="row form-group">
                <div class="col-12">
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="addressInput">Address</label>
                            <TextInput id="addressInput" v-model="listing.address" :value="listing.address" :validator="$v.listing.address" errorMessage="Address is required" placeholder="Address"/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="cityInput">City</label>
                            <TextInput id="cityInput" v-model="listing.city" :value="listing.city" :validator="$v.listing.city" errorMessage="City is required" placeholder="City"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="askingPriceInput">Asking Price</label>
                            <TextInput id="askingPriceInput" v-model="listing.askingPrice" :value="listing.askingPrice" :validator="$v.listing.askingPrice" errorMessage="Asking price must be a valid number" placeholder="Asking Price"/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="yearBuiltInput">Year Built</label>
                            <TextInput id="askingPriceInput" v-model="listing.yearBuilt" :value="listing.yearBuilt" :validator="$v.listing.yearBuilt" errorMessage="Year built must be a valid year" placeholder="Year Built"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="bathroomCountInput">Bathroom Count</label>
                            <select id="bathroomCountInput" class="form-control" v-model="listing.bathroomCount">
                                <option v-for="(value, index) in [0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12 ]" :key="index" :value="value">
                                    {{ value }}
                                </option>
                            </select>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="bedroomCountInput">Bedroom Count</label>
                            <select id="bedroomCountInput" class="form-control" v-model="listing.bedroomCount">
                                <option v-for="(value, index) in 15" :key="index" :value="index">
                                    {{ value }}
                                </option>
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="squareFeetInput">Square Feet</label>
                            <TextInput id="squareFeetInput" v-model="listing.squareFeet" :value="listing.squareFeet" :validator="$v.listing.squareFeet" errorMessage="Square footage must be a valid number" placeholder="Square Footage"/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 pb-3">
                            <label for="lotSizeInput">Lot Size</label>
                            <TextInput id="lotSizeInput" v-model="listing.lotSize" :value="listing.lotSize" :validator="$v.listing.lotSize" errorMessage="Lot size must be a valid number" placeholder="Lot Size"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 pb-3">                            
                            <label for="mainImageInput">Main Image</label>
                            <TextInput id="squareFeetInput" v-model="listing.mainImageUrl" :value="listing.mainImageUrl" :validator="$v.listing.mainImageUrl" errorMessage="Main image URL required" placeholder="Main Image URL"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <p class="p-1">Images</p>
                            <div v-for="(image, index) in listing.images" :key="index" class="row pb-3">
                                <div class="col-lg-2 col-md-3 col-sm-12">
                                    <input class="form-control" type="text" v-model="image.alternative" placeholder="Alternative" required>
                                </div>
                                <div class="col-lg-9 col-md-8 col-sm-12">
                                    <input class="form-control" type="text" v-model="image.url" placeholder="URL" required>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-12">
                                    <button type="button" class="btn btn-link" @click="removeImage(index)">Delete</button>
                                </div>
                            </div>
                            <div class="row">
                                <button type="button" class="btn btn-link" @click="addImage()">Add</button>   
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 pb-3">
                            <label for="descriptionInput">Description</label>
                            <TextAreaInput id="descriptionInput" v-model="listing.description" :value="listing.description" :validator="$v.listing.description" errorMessage="Description required" placeholder="Description"/>
                        </div>
                    </div>
                </div>
            </div>
            <button type="submit" class="btn btn-main bg-blue fade-on-hover text-uppercase"> {{ action }}</button>
        </form>
    </div>
</template>

<script>
import TextInput from '@/components/UI/Form/Text.vue'
import TextAreaInput from '@/components/UI/Form/TextArea.vue'

import { required, helpers } from 'vuelidate/lib/validators'
const postalCodeRegex = helpers.regex('postalCodeRegex', /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/)
const numberOnlyRegex = helpers.regex('numberOnlyRegex', /^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$/)

export default {
    name: 'editListing',
    components: {
        TextInput,
        TextAreaInput
    },
    data() {
		return {
            listing: {
                address: '',
                city: '',
                provinceCode: '',
                postalCode: '',
                askingPrice: '',
                bedroomCount: '',
                yearBuilt: '',
                squareFeet: '',
                lotSize: '',
                description: '',
                mainImageUrl: '',
            },
			error: false,
		}
    },
    computed: {
        action(){
            return this.$route.params.id != null ? 'Edit' : 'Save'
        },
    },
    created() {
        if (this.$route.params.id) {
            this.getListing()
        } else if (this.$route.params.id == null && this.$route.name === 'editListing') {
            this.$router.push({ name: 'listingDashboard' })
        }
    },
    methods: {
        submit() {
            if (this.$route.params.id) {
                console.log('Edit')
            } else {
                console.log('Save')
            }
        },
        getListing() {
            this.$store.dispatch("listings/listingById", this.$route.params.id)
                .then((result) => {
                    this.listing = result
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        },
        removeImage(index) {
            this.listing.images.splice(index, 1)
        },
        addImage() {
            this.listing.images.push({ url: '', alternative: '' })
        }
    },
	validations: {
        listing: {
            address: {
                required
            },
            city: {
                required
            },
            postalCode: {
                required,
                postalCodeRegex
            },
            askingPrice: {
                required,
                numberOnlyRegex
            },
            yearBuilt: {
                required,
                numberOnlyRegex
            },
            squareFeet: {
                required,
                numberOnlyRegex
            },
            lotSize: {
                required,
                numberOnlyRegex
            },
            description: {
                required
            },
            mainImageUrl: {
                required
            }
        }
	},
}
</script>

<style lang="scss" scoped>
select {
    text-align: center;
    text-align-last: center;
}
</style>
