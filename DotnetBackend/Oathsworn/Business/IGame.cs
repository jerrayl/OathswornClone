using System.Threading.Tasks;
using Oathsworn.Models;

namespace Oathsworn.Business
{
    public interface IGame
    {
        int StartEncounter();
        void CreatePlayer(CreatePlayerModel createPlayerModel);

        AttackResponseModel StartAttack(int encounterId, AttackModel attackModel);
        AttackResponseModel RerollAttack (int encounterId, RerollModel rerollModel);
        Task CompleteAttack(int encounterId, int attackId);
        Task Move(int encounterId, MoveModel moveModel);
        Task SpendToken(int encounterId, SpendTokenModel spendTokenModel);
        Task EndTurn(int encounterId);
    }
}
