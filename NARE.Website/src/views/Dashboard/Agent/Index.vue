<template>
    <div v-if="this.$route.name === 'agentDashboard' " class="pt-3 table-responsive">
        <h2 class="text-center">Agents</h2 >

        <SearchBar class="col-12" :submit="searchEmail"/>
        <div class="text-right col-1 offset-11">
            <i class="fas fa-sync-alt pointer" @click="getAllAgents"></i>
        </div>

        <div>
            <h5 v-if="error" class="text-danger">Failed to load agents</h5>
        </div>

        <table border="1" class="table table-bordered table-hover text-center">
            <thead class="thead-dark">
                <tr>
                    <th class="header">Email</th>
                    <th class="">Date Joined</th>
                    <th class="header">Account Enabled</th>
                    <th class="header">Management</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="agent in agents" :key="agent.id" @click="viewDetailedAgent(agent.id)" class="pointer">
                    <td>{{ agent.email }}</td>
                    <td>{{ agent.dateJoined.substr(0, 10) }}</td>
                    <td class="upper">{{ agent.accountEnabled }}</td>
                    <td><button class="btn btn-main bg-blue fade-on-hover" @click="viewDetailedAgent(agent.id)">Edit</button></td>
                </tr>
            </tbody>
        </table>
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
            <li class="page-item ml-auto">
                <router-link :to="{ name: 'newAgent' }" class="btn btn-main bg-blue fade-on-hover">New Agent</router-link>
            </li>
        </ul>
    </div>
    <div v-else>
        <router-view></router-view>
    </div>
</template>

<script>
import SearchBar from '@/components/UI/SearchBar.vue'

export default {
    name: 'AgentDashboard',
    components: {
        SearchBar
    },
    data() {
        return {
            agents: [],
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
        searchEmail(event) {
            let email = event.target[0].value
            if (email === '' || email === null) {
                this.getAllAgents()
                return
            }
            this.$store.dispatch("agents/agentByEmail", email)
                .then((result) => {
                    this.agents = result.agents
                    this.pageCount = result.paginationModel.totalPages
                    this.error = false
                })
                .catch(() => {
                    this.error = true
                })
        },
        viewDetailedAgent(id) {
            this.$router.push({ name: 'agentDetails', params: { id: id } })
        },
        getAllAgents() {
            this.$store.dispatch("agents/agents", { currentPage: this.currentPage, pageSize: 10})
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
            this.getAllAgents()
        }
    },
    created() {
        this.getAllAgents()
    }
}
</script>

<style lang="scss" scoped>
.upper::first-letter {
    text-transform: capitalize;
}
</style>
