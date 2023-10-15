using System.Collections.Generic;
using Oathsworn.Models;

namespace Oathsworn.Business
{
    public interface IGame
    {
        int StartEncounter();
        GameStateModel GetGameState(int encounterId);
        AttackResponseModel StartAttack(int encounterId, AttackModel attackModel);
        AttackResponseModel RerollAttack (int encounterId, RerollModel rerollModel);
        GameStateModel CompleteAttack(int encounterId, int attackId);
        GameStateModel Move(int encounterId, MoveModel moveModel);
    }
}
