import Vue from 'vue'
Vue.config.productionTip = false

Vue.component('hello-world', require('./components/HelloWorld.vue').default);
Vue.component('stock-position-calculator', require('./components/StockPositionCalculator.vue').default);
Vue.component('iron-condor-calculator', require('./components/IronCondorCalculator.vue').default);
Vue.component('weekly-income-settings', require('./components/WeeklyIncomeSettings.vue').default);


window.Vue = Vue;


