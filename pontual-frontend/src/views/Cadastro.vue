<template>
    <v-container>
      <h1 class="text-center font-weight-light">CADASTRO DE CLIENTE</h1>
      <CadastroClientes />
      
        <v-checkbox v-model="chkManterNaPagina" label="Manter na página" class="float-right"></v-checkbox>
    </v-container>

</template>

<script>
import CadastroClientes from '../components/CadastroClientes'
export default {
    
  name: 'App',
  data(){
    return {
      chkManterNaPagina: false,
    }
  },  
  components: {
    CadastroClientes
  }
};
</script>
