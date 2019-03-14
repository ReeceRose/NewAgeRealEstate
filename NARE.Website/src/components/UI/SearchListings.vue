<template>
    <div class="row border p-3">
        <div class="col-12">
            <div class="row text-center">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <div class="form-group">
                        <label for="city">City: </label>
                        <input type="text" id="city" v-model="city" placeholder="Enter city" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="min-price">Minimum Price: </label>
                        <input type="text" id="min-price" v-model="minimumPrice" placeholder="Enter minimum price" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="bedrooms">Bedrooms: </label>
                        <select id="bedrooms" class="form-control" v-model="bedrooms">
                            <option v-for="(value, index) in 15" :key="index" :value="value" selected>
                                {{ value }}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <div class="form-group">
                        <label for="keywords">Keywords: </label>
                        <input type="text" id="keywords" v-model="keywords" placeholder="Enter any keywords" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="max-price">Maximum Price: </label>
                        <input type="text" id="max-price" v-model="maximumPrice" placeholder="Enter maximum price" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="bathrooms">Bathrooms: </label>
                       <select id="bathrooms" class="form-control" v-model="bathrooms">
                            <option v-for="(value, index) in [0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12 ]" :key="index" :value="value" selected>
                                {{ value }}
                            </option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="form-group text-center">
                <button class="btn btn-main bg-blue fade-on-hover text-uppercase" @click="search">Search</button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'searchListings',
    data() {
        return {
            keywords: '',
            city: '',
            minimumPrice: '',
            maximumPrice: '',
            bedrooms: 1,
            bathrooms: 1,
        }
    },
    methods: {
        search() {

        }
    }
}
</script>

<style lang="scss" scoped>
input::placeholder, select {
    text-align: center;
    text-align-last: center;
}
</style>
