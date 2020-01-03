<template>
  <el-submenu
    v-if="menu.childMents && menu.childMents.length > 0"
    :index="menu.id"
    :popper-append-to-body="false">
    <template slot="title">
     <i :class="menu.icon"></i>
      <span>{{ menu.label }}</span>
    </template>
    <sub-menu v-for="item in menu.childMents" :key="item.id" :menu="item"></sub-menu>
  </el-submenu>
  <el-menu-item v-else :index="menu.id" @click="gotoRouteHandle(menu.id)">
    <i :class="menu.icon"></i>
    <span>{{ menu.label }}</span>
  </el-menu-item>
</template>

<script>
import SubMenu from "./aside_sub_menu";
export default {
  name: "sub-menu",
  props: {
    menu: {
      type: Object,
      required: true
    }
  },
  components: {
    SubMenu
  },
  methods: {
    // 通过menuId与动态(菜单)路由进行匹配跳转至指定路由
    gotoRouteHandle(menuId) {
      
    }
  }
};
</script>