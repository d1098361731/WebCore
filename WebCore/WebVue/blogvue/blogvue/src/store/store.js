import Vue from 'vue'
import Vuex from 'vuex';

Vue.use(Vuex)
export default new Vuex.Store({ // 注意Store的S大写
 state:{
   user:{},
   sidebarFold:'',
   sidebarMenuList:[],
 }
})