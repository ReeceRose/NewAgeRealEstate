<template>
    <div class="container p-5">
        <SearchListings/>
        <Listings :listings="null" class="pt-5"/>
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
    computed: {
        c() {
            return this.currentPage
        }
    },
    methods: {
        viewDetailedListing(id) {
            this.$router.push({ name: 'listing', params: { id: id } })
        },
        getAllListings() {
            this.$store.dispatch("listings", { currentPage: this.currentPage, pageSize: 10})
                .then((result) => {
                    this.agents = result.agents
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
