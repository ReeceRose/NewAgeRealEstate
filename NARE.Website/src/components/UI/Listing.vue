<template>
    <div>
        <img class="img-fluid w-100" :src="post.imageUrl" :alt="post.address" :title="post.address">
        <div class="price">${{post.price}}</div>
        <div class="address">{{ post.address }}</div>
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

.price {
    position: absolute;
    top: 0;
    background: color(primaryBlue);
    opacity: 0.75;
    color: #fff;
    text-align: left;
    padding: 1.2em;

    @include mobile {
        padding: 0.1em;
    }

    @include tablet {
        padding: 0.75em;
    }

    @include desktop {
        padding: 1.2em;
    }
}
.address {
    background: #000;
    color: #fff;
    opacity: 0.7;
}
</style>
