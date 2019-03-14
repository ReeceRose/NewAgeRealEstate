import axios from '@/axios.js'
import utilities from '@/utilities.js'

const authentication = {
    namespaced: true,
    getters: {
        isAdmin: (state, getters, rootState) => {
            return utilities.parseJwt(rootState.global.token).hasOwnProperty("Administrator")
        }    
    },
    actions: {
        login: ({ commit, dispatch }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/login',
                    data: { email: payload.email, password: payload.password },
                })
                    .then((response) => {
                        const token = response.data.token
                        dispatch("global/updateToken", token, { root: true })
                        if (payload.rememberMe) {
                            dispatch("global/updateCookie", token, { root: true })
                        }
                        resolve()
                    })
                    .catch(error => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        logout: ({ commit, dispatch }) => {
            commit("global/removeToken", null, { root: true })
            dispatch("global/updateCookie", null, { root: true })
        },
        create: ({ commit }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'authentication/create',
                    data: { name: payload.name, email: payload.email, password: payload.password }
                })
                    .then(response => {
                        resolve(response)
                    })
                    .catch((error) => {
                        reject(error)
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        verifyIsAdmin: ({ rootGetters }) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: 'admin/verify',
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then(() => {
                        resolve()
                    })
                    .catch(() => {
                        reject()
                    })
            })
        },
    }
}

export default authentication