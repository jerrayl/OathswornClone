using System.Linq;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Models;
using AutoMapper;
using Oathsworn.Business.Bosses;
using Microsoft.AspNetCore.SignalR;
using Oathsworn.SignalR;
using System.Threading.Tasks;

namespace Oathsworn.Business.Services
{
    public interface INotificationService
    {
        Task UpdateGameState(int encounterId);
    }

    public class NotificationService : INotificationService
    {
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IDatabaseRepository<EncounterPlayer> _encounterPlayers;
        private readonly IDatabaseRepository<Boss> _bosses;
        private readonly IMapper _mapper;
        private readonly IBossFactory _bossFactory;

        public NotificationService(
            IHubContext<SignalRHub> hubContext,
            IDatabaseRepository<EncounterPlayer> encounterPlayers,
            IDatabaseRepository<Boss> bosses,
            IMapper mapper,
            IBossFactory bossFactory
        )
        {
            _hubContext = hubContext;
            _encounterPlayers = encounterPlayers;
            _bosses = bosses;
            _mapper = mapper;
            _bossFactory = bossFactory;
        }

        public async Task UpdateGameState(int encounterId)
        {
            var players = _encounterPlayers.Read(x => x.EncounterId == encounterId, x => x.Player);
            var bossEntity = _bosses.ReadOne(x => x.EncounterId == encounterId);
            var boss = _bossFactory.GetBossInstance(bossEntity);
            var bossModel = _mapper.Map<Boss, BossModel>(bossEntity);
            bossModel.Name = boss.Name;
            bossModel.NextAction = boss.GetNextActionText();
            bossModel.Positions = boss.GetBossPositions();

            var model = new GameStateModel()
            {
                Players = players.Select(x => _mapper.Map<EncounterPlayer, PlayerModel>(x)).ToList(),
                Boss = bossModel
            };

            await _hubContext.Clients.Group(encounterId.ToString()).SendAsync("GameState", model);
        }

        public async Task DisplayAttack(int encounterId, DisplayAttackModel model)
        {
            await _hubContext.Clients.Group(encounterId.ToString()).SendAsync("Attack", model);
        }
    }
}
