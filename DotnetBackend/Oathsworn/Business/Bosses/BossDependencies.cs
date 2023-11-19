using AutoMapper;
using Oathsworn.Business.Services;
using Oathsworn.Entities;
using Oathsworn.Repositories;

namespace Oathsworn.Business.Bosses
{
    public interface IBossDependencies
    {
        IDatabaseRepository<Encounter> Encounters { get; }
        IDatabaseRepository<EncounterPlayer> EncounterPlayers { get; }
        IDatabaseRepository<EncounterMightDeck> EncounterMightDecks { get; }
        IDatabaseRepository<Boss> Bosses { get; }
        IDatabaseRepository<BossAction> BossActions { get; }
        IDatabaseRepository<BossAttack> BossAttacks { get; }
        IDatabaseRepository<BossAttackPlayer> BossAttackPlayers { get; }
        IMightCardsService MightCardsService { get; }
        IMapper Mapper { get; }
    }

    public class BossDependencies : IBossDependencies
    {
        public IDatabaseRepository<Encounter> Encounters { get; init; }
        public IDatabaseRepository<EncounterPlayer> EncounterPlayers { get; init; }
        public IDatabaseRepository<EncounterMightDeck> EncounterMightDecks { get; init; }
        public IDatabaseRepository<Boss> Bosses { get; init; }
        public IDatabaseRepository<BossAction> BossActions { get; init; }
        public IDatabaseRepository<BossAttack> BossAttacks { get; init; }
        public IDatabaseRepository<BossAttackPlayer> BossAttackPlayers { get; init; }
        public IMightCardsService MightCardsService { get; init; }
        public IMapper Mapper { get; init; }

        public BossDependencies(
            IDatabaseRepository<Encounter> encounters,
            IDatabaseRepository<EncounterPlayer> encounterPlayers,
            IDatabaseRepository<EncounterMightDeck> encounterMightDecks,
            IDatabaseRepository<Boss> bosses,
            IDatabaseRepository<BossAction> bossActions,
            IDatabaseRepository<BossAttack> bossAttacks,
            IDatabaseRepository<BossAttackPlayer> bossAttackPlayers,
            IMightCardsService mightCardsService,
            IMapper mapper
        )
        {
            Encounters = encounters;
            EncounterPlayers = encounterPlayers;
            EncounterMightDecks = encounterMightDecks;
            Bosses = bosses;
            BossActions = bossActions;
            BossAttacks = bossAttacks;
            BossAttackPlayers = bossAttackPlayers;
            MightCardsService = mightCardsService;
            Mapper = mapper;
        }
    }
}