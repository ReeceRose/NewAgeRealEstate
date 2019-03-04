import Vue from 'vue'
import Vuex from 'vuex'

import authentication from '@/store/modules/authentication.js'
import global from '@/store/modules/global.js'
import listings from '@/store/modules/listings.js'
import users from '@/store/modules/users.js'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        authentication,
        global,
        listings,
        users,
    }
})