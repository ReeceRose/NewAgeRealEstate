import axios from '@/axios.js'

const listings  = {
    namespaced: true,
    state: {

    },
    getters: {

    },
    mutations: {

    },
    actions: {        
        listingCount: ({ commit, rootGetters }) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'get',
                    url: 'listings/count',
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        listings: ({ commit, rootGetters }, payload) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'listings/',
                    data: {
                        CurrentPage: payload.currentPage,
                        PageSize: payload.pageSize
                    },
                    headers: { Authorization: `Bearer ${rootGetters['global/getToken']}`}
                })
                    .then((response) => {
                        resolve(response.data.result)
                    })
                    .catch(() => {
                        reject()
                    })
                    .finally(() => {
                        commit('global/setLoading', false, { root: true })
                    })
            })
        },
        create: ({ commit }, listing) => {
            return new Promise((resolve, reject) => {
                commit('global/setLoading', true, { root: true })
                axios({
                    method: 'post',
                    url: 'listings/create',
                    data: { listing: listing }
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
    }
}

export default listings