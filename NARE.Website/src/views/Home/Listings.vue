<template>
    <div class="container p-5">
        <SearchListings/>
        <Listings :listings="listings" class="pt-5"/>
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
        </ul>
    </div>
</template>

<script>
import SearchListings from '@/components/UI/SearchListings.vue'
import Listings from '@/components/UI/Home/Listings.vue'

export default {
    name: 'listings',
    components: {
        SearchListings,
        Listings
    },
    data() {
        return {
            listings: [],
            error: false,
            currentPage: 1,
            pageCount: 1
        }
    },
    created() {
        this.getAllListings()
    },
    computed: {
        c() {
            return this.currentPage
        },
        refresh() {
            return this.$store.getters['global/isRefreshing']
        }
    },
    watch: {
        refresh: function() {
            this.getAllListings()
        }
    },
    methods: {
        viewDetailedListing(id) {
            this.$router.push({ name: 'listing', params: { id: id } })
        },
        getAllListings() {
            if (this.$route.params.tempListings != null && this.$route.params.paginationModel != null) {
                this.listings = this.$route.params.tempListings
                this.pageCount = this.$route.params.paginationModel.totalPages || 1
                this.error = false
                return
            }
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
}
</script>
