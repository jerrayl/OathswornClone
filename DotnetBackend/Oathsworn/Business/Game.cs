using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Models;
using Oathsworn.Extensions;
using AutoMapper;

namespace Oathsworn.Business
{
    public class Game : IGame
    {
        private IDatabaseRepository<Encounter> _encounters;
        private IDatabaseRepository<EncounterMightDeck> _encounterMightDecks;
        private IDatabaseRepository<MightCard> _mightCards;
        private IDatabaseRepository<FreeCompany> _freeCompanies;
        private IDatabaseRepository<Player> _players;
        private IDatabaseRepository<EncounterPlayer> _encounterPlayers;
        private IDatabaseRepository<Boss> _bosses;
        private IMapper _mapper;

        public Game(
            IDatabaseRepository<Encounter> encounters,
            IDatabaseRepository<EncounterMightDeck> encounterMightDecks,
            IDatabaseRepository<MightCard> mightCards,
            IDatabaseRepository<FreeCompany> freeCompanies,
            IDatabaseRepository<Player> players,
            IDatabaseRepository<EncounterPlayer> encounterPlayers,
            IDatabaseRepository<Boss> bosses,
            IMapper mapper
        )
        {
            _encounters = encounters;
            _encounterMightDecks = encounterMightDecks;
            _mightCards = mightCards;
            _freeCompanies = freeCompanies;
            _players = players;
            _encounterPlayers = encounterPlayers;
            _bosses = bosses;
            _mapper = mapper;
        }

        public List<MightCardModel> RerollAttack(int encounterId, RerollModel rerollModel)
        {
            throw new NotImplementedException();
        }

        public List<MightCardModel> StartAttack(int encounterId, AttackModel attackModel)
        {
            throw new NotImplementedException();
        }

        public GameStateModel GetGameState(int encounterId)
        {
            var players = _encounterPlayers.Read(x => x.EncounterId == encounterId, x => x.Player);
            var boss = _bosses.ReadOne(x => x.EncounterId == encounterId);

            return new GameStateModel(){
                Players = players.Select(x => _mapper.Map<EncounterPlayer, PlayerModel>(x)).ToList(),
                Boss = _mapper.Map<Boss, BossModel>(boss)
            };
        }

        public int StartEncounter()
        {
            var encounter = new Encounter();
            _encounters.Add(encounter);

            var playerDeck = new EncounterMightDeck()
            {
                EncounterId = encounter.Id,
                IsFreeCompanyDeck = true
            };
            _encounterMightDecks.Add(playerDeck);

            var enemyDeck = new EncounterMightDeck()
            {
                EncounterId = encounter.Id,
                IsFreeCompanyDeck = false
            };
            _encounterMightDecks.Add(enemyDeck);

            var playerMightCards = MightCardsDistribution.MIGHT_CARDS.Select(x =>
                new MightCard
                {
                    Type = x.Type,
                    Value = x.Value,
                    IsCritical = x.IsCritical,
                    DeckId = playerDeck.Id
                }
            ).ToList();
            playerMightCards.Shuffle();
            _mightCards.AddBatch(playerMightCards);

            var enemyMightCards = MightCardsDistribution.MIGHT_CARDS.Select(x =>
                new MightCard
                {
                    Type = x.Type,
                    Value = x.Value,
                    IsCritical = x.IsCritical,
                    DeckId = playerDeck.Id
                }
            ).ToList();
            enemyMightCards.Shuffle();
            _mightCards.AddBatch(enemyMightCards);

            var freeCompany = new FreeCompany() { Name = "Test Free Company" };
            _freeCompanies.Add(freeCompany);

            var player = new Player()
            {
                FreeCompanyId = freeCompany.Id,
                Class = Class.Witch,
                Health = 5,
                Defence = 2,
                MaxAnimus = 8,
                AnimusRegen = 6,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 1 }, { Might.Red, 0 }, { Might.Black, 0 } }
            };
            _players.Add(player);
            var encounterPlayer = new EncounterPlayer()
            {
                EncounterId = encounter.Id,
                PlayerId = player.Id,
                XPosition = 0,
                YPosition = 0,
                CurrentHealth = player.Health,
                CurrentAnimus = player.AnimusRegen,
                Tokens = new() { { Token.Animus, 0 }, { Token.Battleflow, 0 }, { Token.Defence, 0 }, { Token.Empower, 0 }, { Token.Redraw, 0 } }
            };
            _encounterPlayers.Add(encounterPlayer);

            var boss = new Boss()
            {
                EncounterId = encounter.Id,
                Health = new() { { BossPart.Front, 6 }, { BossPart.Back, 6 }, { BossPart.LeftFlank, 6 }, { BossPart.RightFlank, 6 }, { BossPart.Core, 6 } },
                Defence = 2,
                XPosition = 0,
                YPosition = 0,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 3 }, { Might.Red, 2 }, { Might.Black, 0 } }
            };
            _bosses.Add(boss);

            return encounter.Id;
        }
    }
}
