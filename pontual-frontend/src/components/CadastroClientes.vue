<template>
    <v-container>
        <v-form @submit.prevent="cadastraCliente" v-model="valido" ref="form">
            <v-alert :value="exibeAlerta" v-bind:type="tipoAlerta" transition="fade-transition" dismissible>
                {{mensagemAlerta}}
            </v-alert>            
            <v-row>
            <!-- Campos -->
                <v-col cols="12" md="4">
                    <v-text-field v-model="cliente.nomeFantasia" :rules="regrasNomeFantasia" :counter="250" label="Nome Fantasia" required>
                    </v-text-field>
                </v-col>

                <v-col cols="12" md="4">
                    <v-text-field v-model="cliente.razaoSocial" :rules="regrasRazaoSocial" :counter="250" label="Razão Social" required>
                    </v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                    <v-text-field v-model="cliente.cnpj" :rules="regrasCnpj" :counter="20" label="CNPJ" required v-mask="'##.###.###/####-##'">
                    </v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                    <v-text-field v-model="cliente.email" :rules="regrasEmail" :counter="150" label="E-mail" required type="email">
                    </v-text-field>
                </v-col>
                <v-col cols="12" md="4">
                    <v-text-field v-model="cliente.telefone" :rules="regrasTelefone" :counter="30" label="Telefone" required>
                    </v-text-field>
                </v-col>
                <!--/ Campos -->
            </v-row>
            <div class="">
                <v-btn class="mr-4" type="submit" :disabled="!valido">
                    Cadastrar
                </v-btn>
            </div>
        </v-form>
    </v-container>

</template>


<script>
    import { cnpj } from 'cpf-cnpj-validator';
    import clientesService from '../services/clientes';
    export default {
        data(){
            return{
                valido: false,
                tipoAlerta: 'success',
                mensagemAlerta: '',
                exibeAlerta: false,
                cliente: {
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
            cadastraCliente(){
                this.cliente.dataCadastro = new Date().toISOString();
                clientesService.cadastrar(this.cliente).then(resposta => {
                    this.montaAlerta('success', 'Cliente cadastrado com sucesso.');
                    resposta;
                    this.$emit('cadastra-cliente', this.cliente);
                    this.$refs.form.resetValidation();
                    this.$refs.form.reset();
                }).catch(e => {
                    this.montaAlerta('error', `Erro ao tentar cadastrar cliente. Resposta do servidor: "${e.response.data}"`);
                });
            },
            montaAlerta(tipo, mensagem){
                this.mensagemAlerta = mensagem;
                this.tipoAlerta = tipo;
                this.exibeAlerta = true;
                setTimeout(() => this.exibeAlerta = false, 5000);
            }
        }
    }
</script>