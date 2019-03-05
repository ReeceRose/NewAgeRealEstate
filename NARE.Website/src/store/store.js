import Vue from 'vue'
import Vuex from 'vuex'

import agents from '@/store/modules/agents.js'
import authentication from '@/store/modules/authentication.js'
import global from '@/store/modules/global.js'
import listings from '@/store/modules/listings.js'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        agents,
        authentication,
        global,
        listings,
    }
})