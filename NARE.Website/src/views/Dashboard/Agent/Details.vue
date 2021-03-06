<template>
    <div class="container">
        <div class="row">
            <div class="col">
                <h2 class="text-center pb-4">Agent</h2>
                <p v-if="error" class="text-danger text-center">Failed to load agent</p>

                <p v-if="promotedToAdministrator" class="text-success text-center">Promoted to administrator</p>
                <p v-if="promotedToAdministratorError" class="text-danger text-center">Cannot promote to administrator</p>

                <p v-if="revokedAdministrator" class="text-success text-center">Revoked administrative rights</p>
                <p v-if="revokedAdministratorError" class="text-danger text-center">Cannot revoke administrative rights</p>

                <p v-if="accountDisabled" class="text-success text-center">Account has been disabled</p>
                <p v-if="accountEnabledError" class="text-danger text-center">Failed to enable agent</p>

                <p v-if="accountEnabled" class="text-success text-center">Account has been enabled</p>
                <p v-if="accountDisabledError" class="text-danger text-center">Failed to disable agent</p>

                <p v-if="deleteAgentError" class="text-danger text-center">Failed to delete agent</p>

                <p v-if="updatedAgent" class="text-success text-center">Agent has been updated</p>
                <p v-if="updatedAgentError" class="text-danger text-center">Failed to update agent</p>
            </div>
        </div>
        <WideCard :title="agent.email" v-if="agent">
            <div slot="card-content" class="text-center">
                <div class="col-12">
                    <ul>
                        <li><span class="item">
                            <TextInput id="nameInput" v-model="agent.name" :validator="$v.agent.name" errorMessage="Invalid agent name" placeholder="Agent name"/>
                        </span></li>
                        <li class="pt-2"><span class="item">
                            <TextInput id="descriptionInput" v-model="agent.description" :validator="$v.agent.description" errorMessage="Invalid description" placeholder="Agent description"/>
                        </span></li>
                        <li class="pt-2"><span class="item">
                            <TextInput id="phoneInput" v-model="agent.phoneNumber" :validator="$v.agent.phoneNumber" errorMessage="Invalid phone number" placeholder="Agent phone number"/>
                        </span></li>
                        <li class="pt-2"><span class="item">
                            <TextInput id="imageInput" v-model="agent.imageUrl" :validator="$v.agent.imageUrl" errorMessage="Invalid agent URL" placeholder="Agent image URL"/>
                        </span></li>
                        <li><span class="item" v-if="agent.dateJoined">Date Joined: {{ agent.dateJoined.substr(0, 10) }}</span></li>
                        <li class="pt-3">
                            <h3>Agent Claims:</h3>
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
                            <span class="item" v-if="isAdministrator"><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="revokeAdministrator()">Revoke administrator</button></span>
                            <span v-else><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="makeAdministrator()">Make administrator</button></span>
                        </li>
                        <li>
                            <span class="item" v-if="agent.accountEnabled"><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="disableAccount()">Disable Account</button></span>
                            <span class="item" v-else><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="enableAccount()">Enable Account</button></span>
                        </li>
                        <li>
                            <span class="item"><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="deleteAgent()">Delete Agent</button></span>
                        </li>
                            <li>
                            <span class="item"><button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="updateAgent()">Update Agent</button></span>
                        </li>
                        <li class="pt-3">
                            <button class="btn btn-main btn-lg btn-block bg-blue fade-on-hover" @click="$router.go(-1)">Return <i class="fas fa-undo"></i></button>
                        </li>
                    </ul>
                </div>
            </div>
        </WideCard>
    </div>
</template>

<script>
import WideCard from '@/components/UI/Card/WideCard.vue'
import TextInput from '@/components/UI/Form/Text.vue'

import { required, helpers } from 'vuelidate/lib/validators'
const phoneRegex = helpers.regex('phoneRegex', /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/)

export default {
    name: 'DetailedAgent',
    components: {
        TextInput,
        WideCard
    },
    data() {
        return {
            agent: false,
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
            deleteAgentError: false,
            updatedAgent: false,
            updatedAgentError: false,
        }
    },
    methods: {
        getAgent(agentId) {
            this.$store.dispatch("agents/agentById", this.agent.id || agentId)
                .then((data) => {
                    this.agent = data.agent
                    this.claims = data.claims
                })
                .catch(() => {
                    this.error = true
                })
        },
        checkIsAdministrator(claim) {
            claim.type == 'Administrator' ? this.isAdministrator = true : null;
        },
        revokeAdministrator() {
            this.$store.dispatch("agents/removeClaim", { agentId: this.agent.id, claim: "Administrator" })
                .then(() => {
                    this.isAdministrator = false
                    this.revokedAdministrator = true
                })
                .catch(() => {
                    this.revokedAdministratorError = true
                })
        },
        makeAdministrator() {
            this.$store.dispatch("agents/addClaim", { agentId: this.agent.id, claim: "Administrator"})
                .then(() => {
                    this.isAdministrator = true
                    this.promotedToAdministrator = true
                })
                .catch(() => {
                    this.promotedToAdministratorError = true
                })
        },
        enableAccount() {
            this.$store.dispatch("agents/enableAccount", this.agent.id)
                .then(() => {
                    this.agent.accountEnabled = true
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
        disableAccount() {
            this.$store.dispatch("agents/disableAccount", this.agent.id)
                .then(() => {
                    this.agent.accountEnabled = false
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
        deleteAgent() {
            this.$store.dispatch("agents/deleteAgent", this.agent.id)
                .then(() => {
                    this.$router.push({ name: 'agentDashboard'})
                })
                .catch(() => {
                    this.deleteAgentError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.deleteAgentError = false
                    }, 3000)
                })
        },
        updateAgent() {
            this.$store.dispatch("agents/updateAgent", this.agent)
                .then(() => {
                    this.updatedAgent = true
                    this.updatedAgentError = false
                })
                .catch(() => {
                    this.updatedAgentError = true
                })
                .finally(() => {
                    setTimeout(() => {
                        this.updatedAgentError = false
                    }, 3000)
                })
        },
    },
    created() {
        this.getAgent(this.$route.params.id)
    },
    validations: {
        agent: {
            name: {
                required
            },
            description: {
                required
            },
            phoneNumber: {
                required,
                phoneRegex
            },
            imageUrl: {
                required
            }
        }
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