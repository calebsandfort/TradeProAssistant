import _ from "lodash";

const state = {
    settings: {
        slots: 5,
        minStrikeDiff: 1,
        maxRisk: 3000,
        strikePadding: 0
    }
};

const getters = {
    capitalRequirements: (state) => {
        return state.settings.maxRisk * state.settings.slots;
    }
};

const mutations = {
    setField(state, payload) {
        const names = payload.name.split(",");

        for (let i = 0; i < names.length; i++) {
            _.set(state, names[i], payload.v);
        }
    }
};

const actions = {
    setField({ commit }, payload) {
        commit("setField", payload);
    }
};

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
};