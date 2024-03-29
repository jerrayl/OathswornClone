import axios from 'axios';
import { AttackModel, MoveModel, RerollModel } from './apiModels';
import { ENCOUNTER_ID } from '../Game/utils/constants';

axios.defaults.withCredentials = true;
const api = axios.create({
    baseURL: 'api/',
    headers: {
        'Content-Type': 'application/json',
    },
});

export const getPlayers = async () => {
    return (await api.get(`players`)).data;
}
export const getFreeCompanies = async () => {
    return (await api.get(`free-companies`)).data;
}
export const getEncounters = async () => {
    return (await api.get(`encounters`)).data;
}

export const startAttack = async (model: AttackModel) => {
    return (await api.post(`start-attack?encounterId=${ENCOUNTER_ID}`, model)).data;
}

export const rerollAttack = async (model: RerollModel) => {
    return (await api.post(`reroll-attack?encounterId=${ENCOUNTER_ID}`, model)).data;
}

export const completeAttack = async (attackId: number) => {
    return (await api.post(`complete-attack?encounterId=${ENCOUNTER_ID}&attackId=${attackId}`)).data;
}

export const move = async (model: MoveModel) => {
    return (await api.post(`move?encounterId=${ENCOUNTER_ID}`, model)).data;
}

export const endTurn = async () => {
    return (await api.post(`end-turn?encounterId=${ENCOUNTER_ID}`)).data;
}

export const continueEnemyAction = async () => {
    return (await api.post(`continue-enemy-action?encounterId=${ENCOUNTER_ID}`)).data;
}