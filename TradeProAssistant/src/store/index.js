import Vue from "vue";
import Vuex from "vuex";
import weeklyIncome from "./modules/weekly-income";
Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        weeklyIncome
    },
    strict: true
});
