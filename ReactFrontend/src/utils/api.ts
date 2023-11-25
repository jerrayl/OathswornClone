import axios from 'axios';
import { AttackModel, MoveModel, RerollModel } from './apiModels';
import { ENCOUNTER_ID, PORT_NUMBER } from './constants';

const api = axios.create({
    baseURL: `https://localhost:${PORT_NUMBER}/api/`,
    headers: {
        'Content-Type': 'application/json',
    },
});

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