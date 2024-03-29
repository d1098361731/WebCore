import Vue from 'vue'
import App from './App'
import router from './router'
import store from './store/store'
import http from './utils/http'
import elementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import '@/styles/index.scss'
Vue.use(elementUI)

Vue.prototype.$http = http
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
