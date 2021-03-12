<template>
    <v-container>    
        <!-- <Cadastro v-on:add-cliente="addCliente" /> -->
        <v-data-table :headers="cabecalho" :items="clientes" :items-per-page="3" class="elevation-1">
        <template v-slot:item.acoes="{ item }">
            <v-icon small class="mr-2" v-on:click="openUpdateDialog(item)">
                mdi-pencil
            </v-icon>
            <v-icon small v-on:click="removeItem(item.id)">
                mdi-delete
            </v-icon>
        </template>
    </v-data-table>

    <!-- Dialog de edição -->
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Editar cliente</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="6">
                <v-text-field label="Nome*" hint="Seu nome" persistent-hint required v-model="clienteEditando.name"></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field label="Telefone*" required v-model="clienteEditando.telefone"></v-text-field>
              </v-col>

            </v-row>
          </v-container>
          <small>Obrigatório *</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text v-on:click="dialog = false">
            Fechar
          </v-btn>
          <v-btn color="blue darken-1" text v-on:click="updateItem">
            Salvar
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- /Dialog de edição -->

    </v-container>

</template>


<script>
    export default{
        data(){
            return{
                dialog: false,
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
                clientes:[
                    {
                        id: '1',
                        name: 'Alex',
                        telefone: '1109841221'
                    }
                ],
                clienteEditando: {
                    id: '',
                    nomeFantasia: '',
                    razaoSocial: '',
                    cnpj: '',
                    email: '',
                    telefone: ''
                },
            }
        },
        methods:{
            removeItem(id){
                console.log(id);
                this.clientes.splice(this.clientes.findIndex(x => x.id == id), 1);
            },
            openUpdateDialog(cliente){
                this.clienteEditando.name = cliente.name;
                this.clienteEditando.telefone = cliente.telefone;
                this.clienteEditando.id = cliente.id;
                this.dialog = true;
            },
            updateItem(){
                Object.assign(this.clientes[this.clientes.findIndex(x => x.id == this.clienteEditando.id)], this.clienteEditando);
                this.clienteEditando = {};
                this.dialog = false;
            },
            addCliente(cliente){
                cliente.id = '10';
                this.clientes.push(cliente);
                cliente = {};
            }
        }
    }
</script>