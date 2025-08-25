import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7113/api'
});

export const getProperties = (filters) => {
    return api.get('/PropertyApi', { params: filters });
};

export const getPropertyDetails = (id) => {
    return api.get(`/PropertyApi/${id}/details`);
};