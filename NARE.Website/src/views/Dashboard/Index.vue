<template>
    <div class="container">
        <ApiHealth/>
        <div v-if="this.$route.name === 'dashboard'" >
            <h1 class="text-left pt-3">
                Agent Dashboard
            </h1>
            <div class="row">
                <HeaderCard title="Agents" class="text-center" :click="agentClick" v-if="this.$store.getters['authentication/isAdmin']">
                    <div slot="card-icon">
                        <i class="fas fa fa-4x fa-users"></i>
                    </div>
                    <div slot="card-content">
                        <p>Agent Count: {{ agentCount }}</p>
                    </div>
                </HeaderCard>
            </div>
        </div>
        <div v-else>
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
import ApiHealth from '@/components/UI/Dashboard/ApiHealth.vue'
import HeaderCard from '@/components/UI/Card/Agent/HeaderCard.vue'

export default {
    name: 'Dashboard',
    components: {
        ApiHealth,
        HeaderCard
    },
    data() {
        return {
            agentCount: 0,
        }
    },
    methods: {
        agentClick() {
            this.$router.push({ name: 'agentDashboard' })
        },
        getAgentCount() {
            this.$store.dispatch("agents/agentCount")
                .then((agentCount) => {
                    this.agentCount = agentCount
                })
                .catch(() => {
                    this.agentCount = 'Failed to load'
                })
        }
    },
    created() {
        this.getAgentCount()
    }
}
</script>