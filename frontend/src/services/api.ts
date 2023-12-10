import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7105'
});

export function setupApi(baseUrl: string | undefined) {
    return axios.create({
        baseURL: baseUrl
    })
}

export default instance;
