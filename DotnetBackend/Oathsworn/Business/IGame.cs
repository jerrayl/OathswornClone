using System.Collections.Generic;
using Oathsworn.Models;

namespace Oathsworn.Business
{
    public interface IGame
    {
        int StartEncounter();
        GameStateModel GetGameState(int encounterId);
        List<MightCardModel> StartAttack(int encounterId, AttackModel attackModel);
        List<MightCardModel> RerollAttack (int encounterId, RerollModel rerollModel);
    }
}
