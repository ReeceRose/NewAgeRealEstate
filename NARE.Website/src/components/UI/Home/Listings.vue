<template>
    <div class="row">
        <div v-for="(post, index) in posts" :key="index" class="col-lg-4 col-md-6 col-sm-12">
            <Listing :post="post"/>
        </div>
    </div>
</template>

<script>
import Listing from '@/components/UI/Listing.vue'

export default {
    name: 'Listings',
    components: {
        Listing
    },
    props: {
        // TODO: Add this back and remove the hard coded data below
        // posts: Object
    },
    data() {
        return {
            posts: [
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                },
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                },
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                },
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                },
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                },
                {
                    address: '123 Test Ave',
                    city: 'Kitchener',
                    province: 'ON',
                    postalCode: 'A1B 2C3',
                    squareFootage: '2750',
                    garage: '3',
                    bedrooms: '4',
                    bathrooms: '2.5',
                    price: '15000',
                    agent: 'Test Agent',
                    postedDate: '01/01/2019',
                    imageUrl: 'https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940'
                }
            ]
        }
    }
}
</script>
