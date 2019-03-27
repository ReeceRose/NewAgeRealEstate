<template>
    <form @submit.prevent="search">
        <div class="row border p-3">
            <div class="col-12 text-center">
                <p class="text-danger" v-if="error">Failed to search for listings</p>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="city">City: </label>
                            <input type="text" id="city" v-model="searchModel.city" placeholder="Enter city" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="keywords">Keywords: </label>
                            <input type="text" id="keywords" v-model="searchModel.keywords" placeholder="Enter any keywords" class="form-control">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="min-price">Minimum Price: </label>
                            <input type="text" id="min-price" v-model="searchModel.minimumPrice" placeholder="Enter minimum price" class="form-control">
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="max-price">Maximum Price: </label>
                            <input type="text" id="max-price" v-model="searchModel.maximumPrice" placeholder="Enter maximum price" class="form-control">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="bathrooms">Bathrooms: </label>
                        <select id="bathrooms" class="form-control" v-model="searchModel.minimumBathrooms">
                                <option v-for="(value, index) in [0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12 ]" :key="index" :value="value" selected>
                                    {{ value }}
                                </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-12">
                        <div class="form-group">
                            <label for="bedrooms">Bedrooms: </label>
                            <select id="bedrooms" class="form-control" v-model="searchModel.minimumBedrooms">
                                <option v-for="(value, index) in 15" :key="index" :value="value" selected>
                                    {{ value }}
                                </option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group text-center">
                    <button type="submit" class="btn btn-main bg-blue fade-on-hover text-uppercase">Search</button>
                </div>
            </div>
        </div>
    </form>
</template>

<script>
export default {
    name: 'searchListings',
    data() {
        return {
            error: false,
            searchModel: {
                keywords: null,
                city: null,
                minimumPrice: null,
                maximumPrice: null,
                minimumBedrooms: 1,
                minimumBathrooms: 1,
            }
        }
    },
    methods: {
        search() {
            var paginationModel = {
                PageSize: 10
            }
            this.$store.dispatch("listings/search", { paginationModel, searchModel: this.searchModel })
                .then((data) => {
                    this.$router.push({ name: 'home' })
                    this.$store.dispatch("global/refreshListings")
                    this.$router.push({ name: 'listings', params: { paginationModel: data.paginationModel, tempListings: data.listings } })
                })
                .catch(() => {
                    this.error = true
                })
        }
    }
}
</script>

<style lang="scss" scoped>
input, select {
    text-align: center;
    text-align-last: center;
}
</style>
