<template>
    <v-form v-model="valid">
        <v-container>
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
            <div class="float-right">
                <v-btn class="mr-4 float-right" v-on:click="submit">
                    Cadastrar
                </v-btn>
            </div>
        </v-container>
    </v-form>
</template>


<script>
    import { cnpj } from 'cpf-cnpj-validator'; 
    export default {
        data(){
            return{
                valid: false,
                cliente: {
                    id: '',
                    nomeFantasia: '',
                    razaoSocial: '',
                    cnpj: '',
                    email: '',
                    telefone: ''
                },
                regrasNomeFantasia: [
                    v => !!v || 'Nome Fantasia é obrigatório',
                    v => v.length <= 250 || 'Nome Fantasia deve ter no máximo 250 caracteres',
                ],
                regrasRazaoSocial: [
                    v => !!v || 'Razão Social é obrigatório',
                    v => v.length <= 250 || 'Razão Social deve ter no máximo 250 caracteres',
                ],
                regrasCnpj: [
                    v => !!v || 'CNPJ é obrigatório',
                    v => cnpj.isValid(v) || 'O CNPJ está inválido',
                    v => v.length <= 20 || 'CNPJ deve ter no máximo 20 caracteres',
                ],
                regrasEmail: [
                    v => !!v || 'E-mail é obrigatório',
                    v => /.+@.+/.test(v) || 'O E-mail está inválido',
                    v => v.length <= 150 || 'E-mail deve ter no máximo 150 caracteres',
                ],
                regrasTelefone: [
                    v => !!v || 'Telefone é obrigatório',
                    v => v.length <= 30 || 'Telefone deve ter no máximo 30 caracteres',
                ],
            } 
        },
        methods: {
            submit(){
                this.$emit('add-cliente', this.cliente);
                this.cliente  = {};
            },
            formatarCnpj(){
                this.cliente.cnpj = cnpj.format(this.cliente.cnpj);
            }
        }
    }
</script>