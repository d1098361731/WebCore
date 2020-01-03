<template style="width:100%;height:100%">
  <el-row type="flex" justify="center">
    <el-card >
      <span>系统登录</span>
    <el-form ref="loginForm" :model="loginModel" :rules="rules" status-icon label-width="50px">
      <el-form-item label="账号" prop="name">
        <el-input v-model="loginModel.name"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input v-model="loginModel.password" type="password"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-upload" @click="login">登录</el-button>
      </el-form-item>
    </el-form>
  </el-card>
  </el-row>
</template>

<script>
import Cookies from 'js-cookie'
export default {
  name: "Login",
  data() {
    return {
      loginModel: {
        name:'',
        password:''
      }, //配合页面内的 prop 定义数据
      // user:{},
      rules: {
        //配合页面内的 prop 定义规则
        name: [{ required: true, message: "用户名不能为空", trigger: "blur" }],
        password: [{ required: true, message: "密码不能为空", trigger: "blur" }]
      }
    };
  },
  methods: {
    login() {
      var _this =this
      //使用elementui validate验证
      _this.$refs.loginForm.validate(valid => {
        if (valid) {
          //这里在下边会改写成登录信息 感谢 @风格不同 提醒注释错误问题
          //{userName:_this.user.name,password:_this.user.password}
          _this.$http
            .get("/api/user/login",_this.loginModel )
            .then(res => {
              _this.$store.state.user = res.data
              Cookies.set('token',_this.$store.state.user.token)
              _this.$notify({
                type: "success",
                message: "欢迎你," + res.data.name + "!",
                duration: 3000
              });
              _this.$router.replace("/");
            })
            .catch(arr => {
              _this.$message({
                type: "error",
                message: "用户名或密码错误",
                showClose: true
              });
            });
        } else {
          return false;
        }
      });
    }
  }
};
</script>