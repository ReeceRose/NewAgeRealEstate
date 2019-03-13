<template>
    <div class="container">
        <a @click="$router.go(-1)" class="btn btn-link"><i class="fas fa-arrow-left"></i> Back</a>
        <WideCard :title="agent.name">
            <div slot="card-content" v-if="agent.name">
                <div class="agent text-center">
                    <img class="img-fluid" :src="agent.imageUrl" :alt="agent.name">
                    <p class="pt-2">Inquire here</p>
                    <hr>
                    <p><a :href="`tel:${agent.phoneNumber}`"><i class="fas fa-phone"></i> {{ agent.phoneNumber }}</a></p>
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
    methods: {
        loadAgentInformation() {
            this.$store.dispatch("agents/agentById", this.$route.params.id)
                .then((data) => {
                    console.log(JSON.stringify(data.agent))
                    this.agent = data.agent
                })
                .catch(() => {
                    this.error = true
                })
        }
    },
    data() {
        return {
            agent: {},
        }
    },
    created() {
        this.loadAgentInformation()
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
