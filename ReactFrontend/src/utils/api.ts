import axios from 'axios';
import { AttackModel, RerollModel } from './apiModels';

const api = axios.create({
    baseURL: `https://localhost:5001/api/`,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const startAttack = async (model: AttackModel) => {
    return (await api.post(`start-attack?encounterId=${1}`, model)).data;
}

export const rerollAttack = async (model: RerollModel) => {
    return (await api.post(`reroll-attack?encounterId=${1}`, model)).data;
}

export const completeAttack = async (attackId: number) => {
    return (await api.post(`complete-attack?encounterId=${1}&attackId=${attackId}`)).data;
}