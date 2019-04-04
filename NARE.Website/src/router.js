import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'
import utilities from '@/utilities.js'

// Lazy load all imports
const About = () => import('@/views/Home/About.vue')
const AccessDenied = () => import('@/views/Home/AccessDenied.vue')
const Agent = () => import('@/views/Home/Agent.vue')
const Home = () => import('@/views/Home/Index.vue')
const Listing = () => import('@/views/Home/Listing.vue')
const Listings = () => import('@/views/Home/Listings.vue')
const SessionExpired = () => import('@/views/Home/SessionExpired.vue')

// Dashboard
const DashboardIndex = () => import('@/views/Dashboard/Index.vue')
const DashboardHome = () => import('@/views/Dashboard/Home.vue')
// Agent - Dashboard
const AgentDashboardIndex = () => import('@/views/Dashboard/Agent/Index.vue')
const AgentDashboardHome = () => import('@/views/Dashboard/Agent/Home.vue')
const AgentDetails = () => import('@/views/Dashboard/Agent/Details.vue')
const NewAgent = () => import('@/views/Dashboard/Agent/NewAgent.vue')
// Listing - Dashboard
const ListingDashboardIndex = () => import('@/views/Dashboard/Listing/Index.vue')
const ListingDashboardHome = () => import('@/views/Dashboard/Listing/Home.vue')
const ModifyListing = () => import('@/views/Dashboard/Listing/Modify.vue')

// Agent
const AgentIndex = () => import('@/views/Home/Agent/Index.vue')
const AgentHome = () => import('@/views/Home/Agent/Home.vue')
const Login  = () => import('@/views/Home/Agent/Login.vue')
const ResetPassword = () => import('@/views/Home/Agent/ResetPassword.vue')

Vue.use(Router)

const AgentProtected = {
    beforeEnter: (to, from, next) => {
        const redirect = () => {
            const token = store.getters['global/getToken']
            if (token) {
                next()
            } else {
                next({ name: 'login', params: { redirect: to.fullPath }})
            }
        }
        if (store.getters['global/isLoading']) {
            store.watch(
                (getters) => {
                    getters['global/isLoading']
                },
                () => {
                    redirect()
                }
            )
        } else {
            redirect()
        }
    }
}

const AdminProtected = {
    beforeEnter: (to, from, next) => {
        const redirect = () => {
            const token = store.getters['global/getToken']
            if (token) {
                if (store.getters['authentication/isAdmin']) {
                    next()
                } else {
                    next('/AccessDenied')
                }
            } else {
                next({ name: 'login', params: { redirect: to.fullPath }})
            }
        }
        if (store.getters['global/isLoading']) {
            store.watch(
                (getters) => {
                    getters['global/isLoading']
                },
                () => {
                    redirect()
                }
            )
        } else {
            redirect()
        }
    }
}

const NotLoggedIn = {
    beforeEnter: (to, from, next) => {
        const token = store.getters['global/getToken']
        if (token) {
            next({ name: 'home' })
        }
        else {
            next()
        }
    }
}

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/About',
            name: 'about',
            component: About
        },
        {
            path: '/AccessDenied',
            name: 'accessDenied',
            component: AccessDenied
        },
        {
            path: '/AgentDetails/:id',
            name: 'agent',
            component: Agent
        },
        {
            path: '/Listing/:id',
            name: 'listing',
            component: Listing,
            props: true
        },
        {
            path: '/Listings',
            name: 'listings',
            component: Listings,
            props: true
        },
        {
            path: '/SessionExpired',
            name: 'sessionExpired',
            component: SessionExpired
        },
        {
            path: '/Dashboard',
            component: DashboardIndex,
            ...AgentProtected,
            children: [
                {
                    path: '',
                    name: 'dashboard',
                    component: DashboardHome
                },
                {
                    path: 'Agent',
                    component: AgentDashboardIndex,
                    ...AdminProtected,
                    children: [
                        {
                            path: '',
                            name: 'agentDashboard',
                            component: AgentDashboardHome
                        },
                        {
                            path: 'Details/:id',
                            name: 'agentDetails',
                            component: AgentDetails
                        },                        
                        {
                            path: 'NewAgent',
                            name: 'newAgent',
                            component: NewAgent
                        }
                    ]
                },
                {
                    path: 'Listing',
                    component: ListingDashboardIndex,
                    children: [
                        {
                            path: '',
                            name: 'listingDashboard',
                            component: ListingDashboardHome
                        },
                        {
                            path: 'Edit/:id?',
                            name: 'editListing',
                            component: ModifyListing
                        },                        
                        {
                            path: 'New',
                            name: 'newListing',
                            component: ModifyListing
                        }
                    ]
                }
            ]
        },
        {
            path: '/Agent',
            component: AgentIndex,
            children: [
                {
                    path: '',
                    name: 'agentHome',
                    component: AgentHome
                },
                {
                    path: 'Login/:redirect?',
                    name: 'login',
                    component: Login,
                    ...NotLoggedIn
                },
                {
                    path: 'ResetPassword',
                    name: 'resetPassword',
                    component: ResetPassword
                }
            ]
        },
        {
            path: '*',
            component: Home
        }
    ]
})

router.beforeEach((to, from, next) =>  {
    if (store.getters['global/getToken']) {
        let token = store.getters['global/getToken']
        let parsedToken = utilities.parseJwt(JSON.stringify(token))
        var result = store.dispatch("global/checkExpiration", parsedToken.exp, { root: true })
        if (result) {
            next()
        } else {
            store.dispatch("authentication/logout", { root: true })
            next({ name: 'sessionExpired' })
        }
    }
    next()
})

export default router