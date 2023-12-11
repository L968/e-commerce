import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7105'
});

instance.interceptors.request.use(config => {
    const token = localStorage.getItem('SESSIONJWT');

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
})

export function setupApi(baseUrl: string | undefined) {
    return axios.create({
        baseURL: baseUrl
    })
}

export default instance;
