<template>
    <div class="pt-3">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">User</h2>
                <p v-if="error" class="text-danger text-center">Failed to load user</p>

                <p v-if="promotedToAdministrator" class="text-success text-center">Promoted to administrator</p>
                <p v-if="promotedToAdministratorError" class="text-danger text-center">Cannot promote to administrator</p>

                <p v-if="revokedAdministrator" class="text-success text-center">Revoked administrative rights</p>
                <p v-if="revokedAdministratorError" class="text-danger text-center">Cannot revoke administrative rights</p>

                <p v-if="accountDisabled" class="text-success text-center">Account has been disabled</p>
                <p v-if="accountEnabledError" class="text-danger text-center">Failed to enable user</p>

                <p v-if="accountEnabled" class="text-success text-center">Account has been enabled</p>
                <p v-if="accountDisabledError" class="text-danger text-center">Failed to disable user</p>

                <p v-if="deleteUserError" class="text-danger text-center">Failed to delete user</p>
            </div>
        </div>
        <WideCard :title="user.email" v-if="user">
            <div slot="card-content" class="text-center">
                <div class="col-12">
                    <ul>
                        <li><span class="item" v-if="user.dateJoined">Date Joined: {{ user.dateJoined.substr(0, 10) }}</span></li>
                        <li class="pt-3">
                            <h3>User Claims:</h3>
                            <span v-if="claims.length > 0">
                                <span v-for="(claim, index) in claims" :key="index">
                                    {{ checkIsAdministrator(claim) }}
                                    {{ claim.type }}
                                </span>
                            </span>
                            <span v-else>
                                No special claims
                            </span>
                        </li>
                        <li class="pt-3">
                            <span class="item" v-if="isAdministrator"><button class="btn bg-blue fade-on-hover" @click="revokeAdministrator(user.id)">Revoke administrator</button></span>
                            <span v-else><button class="btn bg-blue fade-on-hover" @click="makeAdministrator(user.id)">Make administrator</button></span>
                        </li>
                        <li>
                            <span class="item" v-if="user.accountEnabled"><button class="btn bg-blue fade-on-hover" @click="disableAccount(user.id)">Disable Account</button></span>
                            <span class="item" v-else><button class="btn bg-blue fade-on-hover" @click="enableAccount(user.id)">Enable Account</button></span>
                        </li>
                        <li>
                            <span class="item"><button class="btn bg-blue fade-on-hover" @click="deleteUser(user.id)">Delete User</button></span>
                        </li>
                        <li class="pt-3">
                            <button class="btn bg-blue fade-on-hover" @click="previous">Return <i class="fas fa-undo"></i></button>
                        </li>
                    </ul>
                </div>
            </div>
        </WideCard>
    </div>
</template>

<script>
import WideCard from '@/components/UI/Card/WideCard.vue'

export default {
    name: 'DetailedUser',
    components: {
        WideCard
    },
    data() {
        return {
            user: false,
            claims: false,
            isAdministrator: false,
            error: false,
            promotedToAdministrator: false,
            promotedToAdministratorError: false,
            revokedAdministrator: false,
            revokedAdministratorError: false,
            accountDisabled: false,
            accountDisabledError: false,
            accountEnabled: false,
            accountEnabledError: false,
            deleteUserError: false,
        }
    },
    methods: {
        getUser(userId) {
            this.$store.dispatch("users/userById", userId)
                .then((data) => {
                    this.user = data.user
                    this.claims = data.claims
                })
                .catch(() => {
                    this.error = true
                })
        },
        checkIsAdministrator(claim) {
            claim.type == 'Administrator' ? this.isAdministrator = true : null;
        },
        revokeAdministrator(userId) {
            this.$store.dispatch("users/removeClaim", { userId, claim: "Administrator" })
                .then(() => {
                    this.isAdministrator = false
                    this.revokedAdministrator = true
                })
                .catch(() => {
                    this.revokedAdministratorError = true
                })
        },
        makeAdministrator(userId) {
            this.$store.dispatch("users/addClaim", { userId, claim: "Administrator"})
                .then(() => {
                    this.isAdministrator = true
                    this.promotedToAdministrator = true
                })
                .catch(() => {
                    this.promotedToAdministratorError = true
                })
        },
        enableAccount(userId) {
            this.$store.dispatch("users/enableAccount", userId)
                .then(() => {
                    this.user.accountEnabled = true
                    this.accountEnabled = true
                })
                .catch(() => {
                    this.accountEnabledError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.accountEnabled = false 
                        this.accountEnabledError = false
                    }, 3000)
                })
        },
        disableAccount(userId) {
            this.$store.dispatch("users/disableAccount", userId)
                .then(() => {
                    this.user.accountEnabled = false
                    this.accountDisabled = true
                })
                .catch(() => {
                    this.accountDisabledError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.accountDisabled = false 
                        this.accountDisabledError = false
                    }, 3000)
                })
        },
        deleteUser(userId) {
            this.$store.dispatch("users/deleteUser", userId)
                .then(() => {
                    this.$router.push({ name: 'userDashboard'})
                })
                .catch(() => {
                    this.deleteUserError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.deleteUserError = false
                    }, 3000)
                })
        },
        previous() {
            this.$router.go(-1)
        }
    },
    created() {
        this.getUser(this.$route.params.id)
    }
}
</script>

<style lang="scss" scoped>
ul {
    list-style: none;

    .item {
        font-size: 1.2rem;

        .btn {
            margin: 5px 5px;
        }
    }
}
</style>
