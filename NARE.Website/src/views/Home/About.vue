<template>
<div>
    <div class="about-header">
        <h1 class="font-main font-bold">New Age Real Estate</h1>
    </div>
    <div class="container pb-5 text-center">
        <h1 class="font-main">About New Age Real Estate</h1>
        <div class="row mt-3 mb-3 border">
            <div class="col">
                <img class="team img-fluid rounded" src="https://static.reecerose.com/images/projects/NewAgeRealEstate/team.jpeg" alt="Our team!">
                <p class="font-italic text-muted pt-2">Our team is full of dedicated professionals who have decades of experience in real estate. Located in Oakville, Ontario we have many listings in and around the Toronto area. We specialize in mid-to-high-end properties.</p>
                <p class="font-italic text-muted">Have any questions? Contact us <a href="mailto:nare@reecerose.com">here</a></p>
            </div>
        </div>
    </div>
</div>
</template>

<style lang="scss" scoped>
.about-header {
    background-image: url("https://static.reecerose.com/images/projects/NewAgeRealEstate/about.jpeg");
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    height: 25vh;
    width: 100%;
    max-width: 100%;
    opacity: 0.95;
    
}
.border {
    padding: 50px;
    border-radius: 35px;
    background: #fafafa;
    .team {
        width: 70%;
        height: auto;
    }
}
</style>