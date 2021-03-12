import Vue from 'vue'
import VueRouter from 'vue-router'
import Listagem from '../views/Listagem'
import Home from '../views/Home'
import Cadastro from '../views/Cadastro'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/listagem',
        name: 'Listagem',
        component: Listagem
    },
    {
        path: '/cadastro',
        name: 'Cadastro',
        component: Cadastro
    },
]

const router = new VueRouter({
  routes
})

export default router
