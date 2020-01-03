<template class="app-side app-side-left"
                :class="isCollapse ? 'app-side-collapsed' : 'app-side-expanded'">
 <!-- 我是样例菜单 -->
          <el-menu :default-active="currentActive"
                   class="el-menu-vertical-demo"
                   background-color="#545c64" text-color="#fff"  active-text-color="#ffd04b"
                   :collapse="false">
            <sub-menu v-for="menu in $store.state.sidebarMenuList" :key="menu.id" :menu="menu"></sub-menu>
          </el-menu>
</template>
<script>
import SubMenu from "./aside_sub_menu";
export default {
   name: "aside",
  data () {
    return {
      currentActive:'',
      isCollapse:true
    }
  },
  components:{
    SubMenu
  },
  methods:{
    getMenu(){
      var that = this;
      if(!that.$store.state.sidebarMenuList||that.$store.state.sidebarMenuList.length===0){
        that.$http.post('/api/Menu/get_menus',{}).then(res=>{
          that.$store.state.sidebarMenuList = res.data;
        }).catch((res,err)=>{
           that.$store.state.sidebarMenuList = [];
        })
      }
    }
  },
  created(){
    this.getMenu()
  }
}
</script>

<style>
  .vertical-demo{
    width: 30%;
    height: 100%;
  }
</style>
