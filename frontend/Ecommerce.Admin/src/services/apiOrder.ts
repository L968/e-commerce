import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7204'
});

instance.interceptors.request.use(config => {
    const token = localStorage.getItem('SESSIONJWT');

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
})

export default instance;
