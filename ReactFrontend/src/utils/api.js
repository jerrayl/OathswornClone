import axios from 'axios';

const api = axios.create({
    baseURL: `https://localhost:5001/api/`,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const getBuildings = async () => {
    return (await api.get('buildings')).data;
}

export const getTroops = async () => {
    return (await api.get('troops')).data;
}

