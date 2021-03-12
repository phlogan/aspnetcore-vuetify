import axios from 'axios'

export const http = axios.create({
    baseURL: 'https://localhost:44321/v1/'
});