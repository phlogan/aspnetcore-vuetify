<template v-model="dialog">

    <!-- Dialog de edição -->
      <v-dialog persistent max-width="600px" v-model="dialog">
        <v-card>
          <v-card-title>
            <span class="headline">Editar cliente</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <!-- Campos -->
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
                <!--/ Campos -->
            </v-container>
            <small>Obrigatório *</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text v-on:click="dialog = false">
              Fechar
            </v-btn>
            <v-btn color="blue darken-1" text>
              Salvar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!-- /Dialog de edição -->

</template>

<script>
    import { cnpj } from 'cpf-cnpj-validator';
    // import clientesService from '../services/clientes';
    export default {
        data(){
            return{
                dialog: true,
                clienteEditando: {
                    id: '',
                    nomeFantasia: '',
                    razaoSocial: '',
                    cnpj: '',
                    email: '',
                    telefone: ''
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
        methods: {
        }
    }
</script>