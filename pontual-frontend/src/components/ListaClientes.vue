<template>
    <v-container>
        <v-data-table :headers="cabecalho" :items="clientes" :items-per-page="10" class="elevation-1">
          <template v-slot:item.acoes="{ item }">
              <v-icon small class="mr-2" v-on:click="abreDialogEdicaoCliente(item)">
                  mdi-pencil
              </v-icon>
              <v-icon small v-on:click="abreDialogRemocaoCliente(item)">
                  mdi-delete
              </v-icon>
          </template>
            <template v-slot:no-data>
              Nenhum cliente encontrado
              <p class="red--text"> {{ mensagemErroListagem }} </p>
          </template>
      </v-data-table>

      <!-- Dialog de edição -->
      <v-dialog persistent max-width="700px" v-model="dialogEdicao">
        <v-form v-on:submit.prevent="alteraCliente" v-model="edicaoValida" ref="form">
            <v-card>
              <v-card-title>
                <span class="headline">Editar cliente</span>
              </v-card-title>
              <v-card-text>
                <v-container>
                <!-- Alerta --> 
                <v-alert :value="exibeAlertaEdicao" v-bind:type="tipoAlertaEdicao" transition="fade-transition" dismissible>
                    {{mensagemAlertaEdicao}}
                </v-alert>
                <!-- /Alerta -->
                  
                  <!-- Campos -->
                  <v-row>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="clienteEditando.nomeFantasia" :rules="regrasNomeFantasia" :counter="250" label="Nome Fantasia" required>
                        </v-text-field>
                    </v-col>

                    <v-col cols="12" md="4">
                        <v-text-field v-model="clienteEditando.razaoSocial" :rules="regrasRazaoSocial" :counter="250" label="Razão Social" required>
                        </v-text-field>
                    </v-col>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="clienteEditando.cnpj" :rules="regrasCnpj" :counter="20" label="CNPJ" required v-mask="'##.###.###/####-##'">
                        </v-text-field>
                    </v-col>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="clienteEditando.email" :rules="regrasEmail" :counter="150" label="E-mail" required type="email">
                        </v-text-field>
                    </v-col>
                    <v-col cols="12" md="4">
                        <v-text-field v-model="clienteEditando.telefone" :rules="regrasTelefone" :counter="30" label="Telefone" required>
                        </v-text-field>
                    </v-col>
                  </v-row>
                    <!--/ Campos -->
                </v-container>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text v-on:click="dialogEdicao=false">
                  Fechar
                </v-btn>
                <v-btn color="blue darken-1" text type="submit" :disabled="!edicaoValida">
                  Salvar
                </v-btn>
              </v-card-actions>
            </v-card>
        </v-form>
      </v-dialog>
      <!-- /Dialog de edição -->

      <!-- Dialog de remoção -->
      <v-dialog v-model="dialogRemocao" width="40%">
        <v-card>
          <v-card-title>
            <span class="headline">Remover cliente</span>
          </v-card-title>
          <v-card-text>
              <h3 class="font-weight-regular">Você tem certeza que deseja remover o cliente "{{clienteRemovendo.nomeFantasia}}"?</h3>
          </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn text v-on:click="dialogRemocao=false">
                Não
              </v-btn>
              <v-btn color="red darken-1" text v-on:click="removeCliente">
                Sim, excluir
              </v-btn>
            </v-card-actions>
        </v-card>
      </v-dialog>
      <!-- /Dialog de remoção -->
    </v-container>

</template>

<script>
  import { cnpj } from 'cpf-cnpj-validator';
  import clientesService from '../services/clientes'
    export default{
        data(){
            return{
                dialogEdicao: false,
                dialogRemocao: false,
                tipoAlertaEdicao: 'success',
                mensagemAlertaEdicao: '',
                exibeAlertaEdicao: false,
                edicaoValida: false,
                mensagemErroListagem: '',
                cabecalho:[
                    { text: 'ID', value: 'id' },
                    { text: 'Nome Fantasia', value: 'nomeFantasia' },
                    { text: 'Razão Social', value: 'razaoSocial' },
                    { text: 'CNPJ', value: 'cnpj' },
                    { text: 'E-mail', value: 'email' },
                    { text: 'Telefone', value: 'telefone' },
                    { text: 'Cadastrado em', value: 'dataCadastro' },
                    {
                        text: 'Ações',
                        value: 'acoes'
                    }
                ],
                clientes:[],
                clienteEditando: {
                    id: '',
                    nomeFantasia: '',
                    razaoSocial: '',
                    cnpj: '',
                    email: '',
                    telefone: '',
                    dataCadastro: ''
                },
                clienteRemovendo: {
                  id: 0,
                  nomeFantasia: ''
                },
                regrasNomeFantasia: [
                    v => !!v || 'Nome Fantasia é obrigatório',
                    v => v != null && v.length <= 250 || 'Nome Fantasia deve ter no máximo 250 caracteres',
                ],
                regrasRazaoSocial: [
                    v => !!v || 'Razão Social é obrigatório',
                    v => v != null && v.length <= 250 || 'Razão Social deve ter no máximo 250 caracteres',
                ],
                regrasCnpj: [
                    v => !!v || 'CNPJ é obrigatório',
                    v => cnpj.isValid(v) || 'O CNPJ está inválido',
                    v => v != null && v.length <= 20 || 'CNPJ deve ter no máximo 20 caracteres',
                ],
                regrasEmail: [
                    v => !!v || 'E-mail é obrigatório',
                    v => /.+@.+/.test(v) || 'O E-mail está inválido',
                    v => v != null && v.length <= 150 || 'E-mail deve ter no máximo 150 caracteres',
                ],
                regrasTelefone: [
                    v => !!v || 'Telefone é obrigatório',
                    v => v != null && v.length <= 30 || 'Telefone deve ter no máximo 30 caracteres',
                ],
            }
        },
        methods:{
            abreDialogEdicaoCliente(cliente){
                this.clienteEditando = cliente;
                this.dialogEdicao = true;
            },
            abreDialogRemocaoCliente(cliente){
                this.clienteRemovendo = cliente;
                this.dialogRemocao = true;
            },
            alteraCliente(){
              this.clienteEditando.dataCadastro = new Date(this.clienteEditando.dataCadastro).toISOString();
              clientesService.alterar(this.clienteEditando).then(resposta => {
                resposta;
                this.clienteEditando = {};
                this.$refs.form.resetValidation();
                this.$refs.form.reset();
                this.dialogEdicao = false;
                this.carregaClientes();
              }).catch(e => {
                  this.montaAlertaEdicao('error', `Erro ao tentar atualizar cliente. Resposta do servidor: "${e.response.data}"`);
              });
            },
            removeCliente(){
              this.clienteRemovendo.dataCadastro = new Date(this.clienteRemovendo.dataCadastro).toISOString();
              clientesService.excluir(this.clienteRemovendo).then(resposta => {
                resposta;
                this.clienteRemovendo = {};
                this.dialogRemocao = false;
                this.carregaClientes();
              }).catch(e => {
                  return `Erro ao tentar remover cliente. Resposta do servidor: "${e.response.data}"`;
              });
            },
            carregaClientes(){
              clientesService.listar().then(resposta => {
                resposta.data.forEach(function(item){
                  item.dataCadastro = new Date(item.dataCadastro).toLocaleString();
                })
                this.clientes = resposta.data;
              }).catch(e => {
                    this.mensagemErroListagem = `${e.response.data}`;
                });
            },
            formataDatas(){
            return this.clientes.forEach(function(item){
              
              console.log(item);
            });
          },
          montaAlertaEdicao(tipo, mensagem){
              this.mensagemAlertaEdicao = mensagem;
              this.tipoAlertaEdicao = tipo;
              this.exibeAlertaEdicao = true;
              setTimeout(() => this.exibeAlertaEdicao = false, 5000);
          }
        },
        mounted(){
          this.carregaClientes();
        },
        computed: {
          
        },
        components: {
        }
    }
</script>