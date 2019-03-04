import Vue from 'vue'
import Router from 'vue-router'
import store from '@/store/store.js'
import utilities from '@/utilities.js'

// Lazy load all imports
const Home = () => import('@/views/Home/Index.vue')
const About = () => import('@/views/Home/About.vue')
const Listings = () => import('@/views/Home/Listings.vue')
const Listing = () => import('@/views/Home/Listing.vue')

// Dashboard
const Dashboard = () => import('@/views/Dashboard/Index.vue')
const UserDashboard = () => import('@/views/Dashboard/Users/Index.vue')
const DetailedUserDashboard = () => import('@/views/Dashboard/Users/DetailedUser.vue')

// USER
const UserIndex = () => import('@/views/Home/User/Index.vue')
const Login  = () => import('@/views/Home/User/Login.vue')

const AccessDenied = () => import('@/views/Home/AccessDenied.vue')
const SessionExpired = () => import('@/views/Home/SessionExpired.vue')

const ResetPassword = () => import('@/views/Home/User/ResetPassword.vue')

Vue.use(Router)

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
            path: '/Listings',
            name: 'listings',
            component: Listings
        },
        {
            path: '/Listing/:id',
            name: 'listing',
            component: Listing,
            props: true
        },
        {
            path: '/User',
            name: 'user',
            component: UserIndex,
            children: [
                {
                    path: 'Login/:redirect?',
                    name: 'login',
                    component: Login,
                    ...NotLoggedIn,
                },
                {
                    path: 'ResetPassword',
                    name: 'resetPassword',
                    component: ResetPassword
                },
            ]
        },
        {
            path: '/Dashboard',
            name: 'dashboard',
            component: Dashboard,
            ...AdminProtected,
            children: [
                {
                    path: 'Users',
                    name: 'userDashboard',
                    component: UserDashboard,
                    children: [
                        {
                            path: ':id',
                            name: 'detailedUserDashboard',
                            component: DetailedUserDashboard
                        }
                    ]
                }
            ]
        },
        {
            path: '/AccessDenied',
            name: 'accessDenied',
            component: AccessDenied
        },
        {
            path: '/SessionExpired',
            name: 'sessionExpired',
            component: SessionExpired
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