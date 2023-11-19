import axios from 'axios';
import { AttackModel, MoveModel, RerollModel } from './apiModels';

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

export const move = async (model: MoveModel) => {
    return (await api.post(`move?encounterId=${1}`, model)).data;
}

export const endTurn = async () => {
    return (await api.post(`end-turn?encounterId=${1}`)).data;
}