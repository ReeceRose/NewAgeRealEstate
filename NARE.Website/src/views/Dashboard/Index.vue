<template>
  <div>
    <ApiHealth/>
    <router-view/>
  </div>
</template>
<script>
import ApiHealth from '@/components/UI/Dashboard/ApiHealth.vue'

export default {
    name: 'dashboardIndex',
    components: {
        ApiHealth
    }
}
</script>
