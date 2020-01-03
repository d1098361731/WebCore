import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/views/main/HelloWorld'
import Main from '@/views/container/main'
import Foo from '@/views/model/foo'
import Login from '@/views/login'
import Cookies from 'js-cookie'
Vue.use(Router)

 const router =  new Router({
  routes: [
    {
      path: '/',
      name: 'Main',
      component: Main
    },
    {
      path: '/foo',
      name: 'Foo',
      component: Foo
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    }
  ]
});

router.beforeEach((to,from,next)=>{
  var toPath = to.path
  if(toPath==='/login'){
    Cookies.remove('token'); 
  }
  var token = Cookies.get('token');
  if(!token && toPath!=='/login'){
    toPath = '/login'
    next({path:toPath});
  }else{
    next();
  }
  
});

export default router
