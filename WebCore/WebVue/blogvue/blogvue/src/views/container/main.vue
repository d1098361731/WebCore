<template>
  <div id="app">
    <el-container>
     
      <el-container>
        <el-aside width="200px">
           <el-aside class="app-side app-side-left" :class="isCollapse ? 'app-side-collapsed' : 'app-side-expanded'">
        <div class="app-side-logo">
          <img src="@/assets/logo.png" :width="isCollapse ? '60' : '60'" height="60" />
        </div>
          <myAside></myAside>
        </el-aside>
        <el-container>
           <el-header>
            <myHeader></myHeader>
          </el-header>
          <el-main>
            <router-view/>
          </el-main>
        </el-container>
      </el-container>
    <el-container>
      <el-footer>
        <myFooter></myFooter>
      </el-footer>
    </el-container>
    </el-container>
  </div>
</template>

<script>
import myHeader from "@/views/container/header";
import myAside from "@/views/container/aside";
import myFooter from "@/views/container/footer";
export default {
  name: "Main",
  data() {
    return {
      user: {}
    };
  },
  components: {
    myHeader,
    myAside,
    myFooter
  }
};
</script>

