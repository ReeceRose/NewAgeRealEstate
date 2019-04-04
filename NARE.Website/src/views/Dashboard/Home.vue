<template>
  <div class="container">
    <h1 class="text-left pt-3">
      Dashboard
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
      <HeaderCard title="Listings" class="text-center" :click="listingClick">
        <div slot="card-icon">
          <i class="fas fa fa-4x fa-home"></i>
        </div>
        <div slot="card-content">
          <p>Listing Count: {{ listingCount }}</p>
        </div>
      </HeaderCard>
    </div>
  </div>
</template>

<script>
import HeaderCard from '@/components/UI/Card/Agent/HeaderCard.vue'

export default {
  name: 'dashboard',
  components: {
    HeaderCard
  },
  data() {
    return {
      agentCount: 0,
      listingCount: 0,
    }
  },
  methods: {
    agentClick() {
      this.$router.push({ name: 'agentDashboard' })
    },
    listingClick() {
      this.$router.push({ name: 'listingDashboard' })
    },
    getAgentCount() {
      this.$store.dispatch("agents/agentCount")
        .then((agentCount) => {
          this.agentCount = agentCount
        })
        .catch(() => {
          this.agentCount = 'Failed to load'
      })
    },
    getListingCount() {
      var countType = this.$store.getters['authentication/isAdmin'] ? 'count' : 'agentListingCount'
      this.$store.dispatch("listings/listingCount", countType)
        .then((listingCount) => {
            this.listingCount = listingCount;
        })
        .catch(() => {
            this.listingCount = 'Failed to load'
        })
      }
  },
  created() {
    this.getAgentCount()
    this.getListingCount()
  }
}
</script>