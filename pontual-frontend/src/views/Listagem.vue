<template>
    <v-container>
      <h1 class="text-center font-weight-light">CLIENTES</h1>
      <ListaClientes />
    </v-container>

</template>

<script>
import ListaClientes from '../components/ListaClientes'
export default {
    
  name: 'App',
  data(){
    return {
      
    }
  }, 
  methods: {
    teste(){
      alert("Foi");
    }
  },
  components: {
    ListaClientes
  }
};
</script>
