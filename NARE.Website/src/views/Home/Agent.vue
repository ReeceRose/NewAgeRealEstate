<template>
    <div class="container">
        <WideCard :title="agent.name">
            <div slot="card-content">
                <div class="agent text-center">
                    <img class="img-fluid" :src="agent.picture" :alt="agent.name">
                    <p class="pt-2">Inquire here</p>
                    <hr>
                    <p><a :href="`tel:${agent.telephone}`"><i class="fas fa-phone"></i> {{ agent.telephone }}</a></p>
                    <p><a :href="`mailto:${agent.email}`"><i class="fas fa-envelope"></i> {{ agent.email }}</a></p>
                </div>
                <hr>
                <p class="text-center">{{ agent.description }}</p>
            </div>
        </WideCard>
    </div>
</template>

<script>
import WideCard from '@/components/UI/Card/WideCard.vue'

export default {
    name: 'agent',
    components: {
        WideCard
    },
    data() {
        return {
            // TODO: Load this from the API
            agent: {
                id: '456',
                name: 'Jane Doe',
                picture: 'https://images.pexels.com/photos/733872/pexels-photo-733872.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260',
                telephone: '111-222-3333',
                email: 'nare@reecerose.com',
                description: 'Short description here'
            },
        }
    }
}
</script>

<style lang="scss" scoped>
@import "@/assets/scss/global.scss"; 
.fas {
    color: color(primaryBlue);
}
.agent {
    .agent-name {
        font-size: 1.5rem;
        padding: 0.8rem 0.8rem 0 0.8rem;  
    }
}
</style>
