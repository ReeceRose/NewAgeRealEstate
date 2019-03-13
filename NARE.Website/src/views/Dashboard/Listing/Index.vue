<template>
        <div v-if="this.$route.name === 'listingDashboard' " class="pt-3 table-responsive">
        <h2 class="text-center">Listings</h2 >

        <div class="text-right col-1 offset-11">
            <i class="fas fa-sync-alt pointer" @click="getAllListings"></i>
        </div>

        <div>
            <h5 v-if="error" class="text-danger">Failed to load listings</h5>
        </div>

        <table border="1" class="table table-bordered table-hover text-center">
            <thead class="thead-dark">
                <tr>
                    <th class="header">Address</th>
                    <th class="header">Status</th>
                    <th class="header">Listing Date</th>
                    <th class="header">Realtor</th>
                    <th class="header">Management</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="listing in listings" :key="listing.id" @click="viewDetailedListing(listing.id)" class="pointer">
                    <td>{{ listing.address }}</td>
                    <td>{{ listing.status }}</td>
                    <td>{{ listing.listingDate }}</td>
                    <td><router-link :to="{ name: 'agent', params: { id: listing.agent.id } }">{{ listing.agent.name }}</router-link></td>
                    <td><button class="btn btn-main bg-blue fade-on-hover" @click="viewDetailedListing(listing.id)">Edit</button></td>
                </tr>
            </tbody>
        </table>
        <ul class="pagination">
            <li class="page-item" :class="c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(c-1)">Previous</span>
            </li>
            <li class="page-item" :class="c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(1)">First</span>
            </li>
            <li v-for="(page, index) in  pageCount" :key="index" class="page-item" :class="page == c ? 'active' : ''">
                <span class="page-link" @click="setPage(page)">{{ page }}</span>
            </li>
            <li class="page-item" :class="c == pageCount || c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(pageCount)">Last</span>
            </li>
            <li class="page-item" :class="c == pageCount || c == 1 ? 'disabled' : ''">
                <span class="page-link" @click="setPage(c+1)">Next</span>
            </li>
            <li class="page-item ml-auto">
                <router-link :to="{ name: 'newListing' }" class="btn btn-main bg-blue fade-on-hover">New Listing</router-link>
            </li>
        </ul>
    </div>
    <div v-else>
        <router-view></router-view>
    </div>
</template>


<script>
export default {
    name: 'ListingDashboard',
    data() {
        return {
            listings: [
{
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
                    listingDate: '01/01/2019',
                    status: 'For sale',
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
                    ]
                }
            ],
            error: false,
            currentPage: 1,
            pageCount: 1
        }
    },
    computed: {
        c() {
            return this.currentPage
        }
    },
    methods: {
        viewDetailedListing(id) {
            this.$router.push({ name: 'listingDetails', params: { id: id } })
        },
        getAllListings() {
            this.$store.dispatch("listings/listings", { currentPage: this.currentPage, pageSize: 10})
                .then((result) => {
                    this.listings = result.listings
                    this.pageCount = result.paginationModel.totalPages
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        },
        setPage(pageIndex) {
            if (pageIndex <= 0 || pageIndex > this.pageCount) return
            this.currentPage = pageIndex
            this.getAllListings()
        }
    },
    created() {
        this.getAllListings()
    }
}
</script>

<style lang="scss" scoped>
.pointer {
    cursor: pointer;
}
.upper::first-letter {
    text-transform: capitalize;
}
</style>
