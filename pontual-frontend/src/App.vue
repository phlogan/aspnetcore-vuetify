<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
    <v-list dense nav>
          <v-list-item v-for="item in items" :key="item.title" link :to="item.to">
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
    </v-navigation-drawer>

    <v-app-bar app>
      <v-app-bar-nav-icon v-on:click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>Pontual</v-toolbar-title>
    </v-app-bar>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',
  components: {
    
  },

  data(){
    return {
      drawer: true,
      items: [
          { title: 'Home', icon: 'mdi-home', to: '/' },
          { title: 'Listar Clientes', icon: 'mdi-view-list', to: '/listagem' },
          { title: 'Cadastrar Cliente', icon: 'mdi-account-plus', to: '/cadastro' },
        ],
    }
  },
};
</script>
