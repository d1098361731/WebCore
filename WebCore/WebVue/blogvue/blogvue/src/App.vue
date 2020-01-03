<template>
  <div id="app">
     <router-view/>
  </div>
</template>

<script>
import Cookies from 'js-cookie'
export default {
  name: "App",
  data() {
    return {
      user: {}
    };
  },
  methods: {
    getData() {
      var that = this;
      var token = Cookies.get('token');
      if(token){
         that.$http.get("/api/user/tokenLogin",{tokenStr:token} )
            .then(res => {
              that.$store.state.user = res.data
              Cookies.set('token',that.$store.state.user.token)
              that.$router.replace("/");
            })
            .catch(arr => {
             that.$router.replace("/login");
            });
      }else{
        that.$router.replace("/login");
      }
    },
    // 窗口改变大小
    windowResizeHandle () {
      this.$store.state.sidebarFold = document.documentElement['clientWidth'] <= 992 || false
      window.addEventListener('resize', debounce(() => {
        this.$store.state.sidebarFold = document.documentElement['clientWidth'] <= 992 || false
      }, 150))
    },
  },
  created() {
    this.getData();
    this.windowResizeHandle();
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
