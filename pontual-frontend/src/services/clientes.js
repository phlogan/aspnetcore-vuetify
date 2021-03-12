import { http } from './config'

export default{
    listar:() => {
        return http.get('clientes')
    },
    cadastrar:(cliente) =>{
        return http.post('clientes/cadastrar', cliente);
    },
    alterar: (cliente) =>{
        return http.put('clientes/alterar', cliente);
    },
    excluir: (cliente) =>{
        return http.delete('clientes/excluir', {data: cliente });
    }
}