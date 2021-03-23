<template>
    <v-container>
      <h1 class="text-center font-weight-light">CADASTRO DE CLIENTE</h1>
      <CadastroClientes v-on:cadastra-cliente="redirListagem"/>
      <br>
        <v-row align="center" justify="center" style="margin-left: -31px">
          <v-checkbox v-model="chkManterNaPagina" label="Continuar cadastrando"></v-checkbox>
        </v-row>
        
    </v-container>

</template>

<script>
import CadastroClientes from '../components/CadastroClientes'
export default {
    
  name: 'App',
  data(){
    return {
      chkManterNaPagina: true,
    }
  },
  methods: {
    redirListagem(){
      if(!this.chkManterNaPagina)
        window.location.href = '/#/listagem';
    }
  }, 
  components: {
    CadastroClientes
  }
};
</script>
