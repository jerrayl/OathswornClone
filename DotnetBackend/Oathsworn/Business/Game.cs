using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Models;
using Oathsworn.Extensions;
using AutoMapper;
using Oathsworn.Business.Bosses;
using Oathsworn.Business.Constants;
using Oathsworn.Business.Helpers;
using Oathsworn.Business.Services;
using Oathsworn.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Oathsworn.Business
{
    public class Game : IGame
    {
        private readonly IDatabaseRepository<Encounter> _encounters;
        private readonly IDatabaseRepository<EncounterMightDeck> _encounterMightDecks;
        private readonly IDatabaseRepository<MightCard> _mightCards;
        private readonly IDatabaseRepository<FreeCompany> _freeCompanies;
        private readonly IDatabaseRepository<Player> _players;
        private readonly IDatabaseRepository<EncounterPlayer> _encounterPlayers;
        private readonly IDatabaseRepository<Boss> _bosses;
        private readonly IDatabaseRepository<Attack> _attacks;
        private readonly IMapper _mapper;
        private readonly IMightCardsService _mightCardsService;
        private readonly INotificationService _notificationService;
        private readonly IBossFactory _bossFactory;

        public Game(
            IDatabaseRepository<Encounter> encounters,
            IDatabaseRepository<EncounterMightDeck> encounterMightDecks,
            IDatabaseRepository<MightCard> mightCards,
            IDatabaseRepository<FreeCompany> freeCompanies,
            IDatabaseRepository<Player> players,
            IDatabaseRepository<EncounterPlayer> encounterPlayers,
            IDatabaseRepository<Boss> bosses,
            IDatabaseRepository<Attack> attacks,
            IMapper mapper,
            IMightCardsService mightCardsService,
            INotificationService notificationService,
            IBossFactory bossFactory
        )
        {
            _encounters = encounters;
            _encounterMightDecks = encounterMightDecks;
            _mightCards = mightCards;
            _freeCompanies = freeCompanies;
            _players = players;
            _encounterPlayers = encounterPlayers;
            _bosses = bosses;
            _attacks = attacks;
            _mapper = mapper;
            _mightCardsService = mightCardsService;
            _notificationService = notificationService;
            _bossFactory = bossFactory;
        }

        public void CreatePlayer(CreatePlayerModel moveModel)
        {
            throw new NotImplementedException();
        }

        public async Task Move(int encounterId, MoveModel moveModel)
        {
            var player = _encounterPlayers.ReadOne(x => x.EncounterId == encounterId && x.Id == moveModel.PlayerId);

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            if (GridHelper.IsValidPath(new List<IPosition>() { player }.Concat(moveModel.Positions).ToList()) && player.CurrentAnimus >= moveModel.Positions.Count)
            {
                player.CurrentAnimus -= moveModel.Positions.Count;
                var lastPosition = moveModel.Positions.Last();
                player.XPosition = lastPosition.XPosition;
                player.YPosition = lastPosition.YPosition;
                _encounterPlayers.Update(player);
            }

            await _notificationService.UpdateGameState(encounterId);
        }

        public async Task SpendToken(int encounterId, SpendTokenModel spendTokenModel)
        {
            var player = _encounterPlayers.ReadOne(x => x.EncounterId == encounterId && x.PlayerId == spendTokenModel.PlayerId);

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            if (spendTokenModel.Token == Token.Animus && player.Tokens[Token.Animus] > 0)
            {
                player.Tokens[Token.Animus] -= 1;
                player.CurrentAnimus += GlobalConstants.ANIMUS_TOKEN_VALUE;
                _encounterPlayers.Update(player);
            }

            if (spendTokenModel.Token == Token.Battleflow && player.Tokens[Token.Battleflow] > 0)
            {
                //Perform logic
            }

            await _notificationService.UpdateGameState(encounterId);
        }

        public async Task CompleteAttack(int encounterId, int attackId)
        {
            var attack = _attacks.ReadOne(x => x.Id == attackId, x => x.MightCards, x => x.Player, x => x.Player.EncounterPlayer, x => x.Boss);

            if (attack is null)
            {
                throw new Exception("Attack not found");
            }

            if (attack.MightCards.Count(x => !x.IsDrawnFromCritical && x.Value == 0) > GlobalConstants.NUM_ZEROES_TO_MISS)
            {
                await _notificationService.UpdateGameState(encounterId);
            }

            attack.Player.EncounterPlayer.Tokens[Token.Empower] -= attack.EmpowerTokensUsed;
            attack.Player.EncounterPlayer.Tokens[Token.Redraw] -= attack.RerollTokensUsed;
            _encounterPlayers.Update(attack.Player.EncounterPlayer);

            // Assume attack targets boss
            attack.Boss.Health[attack.BossPart] -= (attack.MightCards.Sum(x => x.Value) + attack.BonusDamage) / attack.Boss.Defence;
            if (attack.Boss.Health[attack.BossPart] <= 0)
            {
                attack.Boss.Health[attack.BossPart] = 0;

                // If this pushes boss to next stage, discard action cards
                // Set target to be player who triggered the break
            }
            _bosses.Update(attack.Boss);

            _attacks.Delete(attack);

            await _notificationService.UpdateGameState(encounterId);
        }

        public AttackResponseModel RerollAttack(int encounterId, RerollModel rerollModel)
        {
            var attack = _attacks.ReadOne(x => x.Id == rerollModel.AttackId, x => x.MightCards, x => x.Player, x => x.Player.EncounterPlayer);

            if (attack is null)
            {
                throw new Exception("Attack not found");
            }

            var mightCardIds = attack.MightCards.Select(x => x.Id);
            if (rerollModel.MightCards.Any(x => !mightCardIds.Contains(x)))
            {
                throw new Exception("Card ids not found");
            }

            if (rerollModel.RerollTokensUsed < 1 || attack.Player.EncounterPlayer.Tokens[Token.Redraw] < rerollModel.RerollTokensUsed || rerollModel.MightCards.Count != rerollModel.RerollTokensUsed)
            {
                throw new Exception("Invalid number of reroll tokens");
            }

            attack.RerollTokensUsed += rerollModel.RerollTokensUsed;
            _attacks.Update(attack);

            var cardResult = attack.MightCards
                // intialize to cards not redrawn
                .Where(x => !rerollModel.MightCards.Contains(x.Id))
                .ToList();

            var cardsToRedraw = attack.MightCards
                .Where(x => rerollModel.MightCards.Contains(x.Id))
                .ToList();

            var critCards = _mightCardsService.DrawCardsFromCritCards(cardsToRedraw.First().DeckId, attack.Id, cardsToRedraw);
            while (critCards.Any(x => x.IsCritical))
            {
                cardsToRedraw.AddRange(critCards);
                critCards = _mightCardsService.DrawCardsFromCritCards(cardsToRedraw.First().DeckId, attack.Id, critCards);
            }
            cardResult.AddRange(critCards);

            var nonCritCards = _mightCardsService.DrawCardsFromCards(cardsToRedraw.First().DeckId, attack.Id, cardsToRedraw.Where(x => !x.IsCritical).ToList());
            while (nonCritCards.Any(x => x.IsCritical))
            {
                cardsToRedraw.AddRange(nonCritCards);
                nonCritCards = _mightCardsService.DrawCardsFromCritCards(cardsToRedraw.First().DeckId, attack.Id, nonCritCards);
            }
            cardResult.AddRange(nonCritCards);

            _mightCards.DeleteBatch(cardsToRedraw);

            var cardModels = cardResult
                .Select(x => _mapper.Map<MightCard, MightCardModel>(x))
                .ToList();

            return new AttackResponseModel()
            {
                AttackId = attack.Id,
                CardsDrawn = cardModels
            };
        }

        public AttackResponseModel StartAttack(int encounterId, AttackModel attackModel)
        {
            var player = _players.ReadOne(x => x.Id == attackModel.PlayerId, x => x.Attacks);

            if (player is null || player.Attacks.Any())
            {
                throw new Exception("Invalid player id");
            }

            // Locate Player (in encounter)
            // Locate Enemy (in encounter)
            // Check that Player is able to attack enemy, record parts hit

            if (attackModel.Might.Values.Sum() > GlobalConstants.MAXIMUM_ATTACK_MIGHT_CARDS)
            {
                throw new Exception("Too many might cards");
            }

            if (MightCardsHelper.GetEmpowerTokensNeeded(attackModel.Might, player.Might) != attackModel.EmpowerTokensUsed)
            {
                throw new Exception("Invalid number of empower tokens");
            }

            // Calculate bonus damage that would be done

            var attack = new Attack()
            {
                PlayerId = attackModel.PlayerId,
                // Temporarily hardcode boss as target
                BossId = attackModel.EnemyId,
                // Temporarily hardcode core as target
                BossPart = (BossPart.Core, 1).ConvertToString(),
                // Temporarily hardcode bonus damage as 0
                BonusDamage = 0,
                EmpowerTokensUsed = attackModel.EmpowerTokensUsed
            };
            _attacks.Add(attack);

            var playerMightDeck = _encounterMightDecks
                .ReadOne(x => x.EncounterId == encounterId && x.IsFreeCompanyDeck);

            var cardsDrawn = new List<MightCard>();
            var cardsDrawnFromCrit = new List<MightCard>();

            cardsDrawn.AddRange(_mightCardsService.DrawCards(playerMightDeck.Id, attack.Id, attackModel.Might));

            if (cardsDrawn.Any(x => x.IsCritical))
            {
                cardsDrawnFromCrit.AddRange(_mightCardsService.DrawCardsFromCritCards(playerMightDeck.Id, attack.Id, cardsDrawn));
            }

            while (cardsDrawnFromCrit.Any(x => x.IsCritical))
            {
                cardsDrawn.AddRange(cardsDrawnFromCrit);
                cardsDrawnFromCrit = _mightCardsService.DrawCardsFromCritCards(playerMightDeck.Id, attack.Id, cardsDrawnFromCrit);
            }
            cardsDrawn.AddRange(cardsDrawnFromCrit);

            var cardModels = cardsDrawn.Select(x => _mapper.Map<MightCard, MightCardModel>(x)).ToList();

            return new AttackResponseModel
            {
                AttackId = attack.Id,
                CardsDrawn = cardModels
            };
        }

        public async Task EndTurn(int encounterId)
        {
            // check if all players have accepted end turn

            var encounter = _encounters.ReadOne(x => x.Id == encounterId, x => x.Boss);

            if (encounter is null || encounter.CharacterPerformingAction is not null)
            {
                throw new Exception("Invalid encounter state");
            }

            _bossFactory.GetBossInstance(encounter.Boss).BeginAction();
        }

        public async Task ContinueBossAction(int encounterId)
        {
            var encounter = _encounters.ReadOne(x => x.Id == encounterId, x => x.Boss);

            if (encounter is null || encounter.CharacterPerformingAction is not null)
            {
                throw new Exception("Invalid encounter state");
            }

            _bossFactory.GetBossInstance(encounter.Boss).PerformAction();
        }

        public async Task AcceptBossAttack(int encounterId, int attackId)
        {
            // refresh players
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

            var playerMightCards = MightCardsDistribution.MIGHT_CARDS
                .Select(x => new MightCard
                {
                    Type = x.Type,
                    Value = x.Value,
                    IsCritical = x.IsCritical,
                    DeckId = playerDeck.Id
                })
                .ToList();
            playerMightCards.Shuffle();
            _mightCards.AddBatch(playerMightCards);

            var enemyMightCards = MightCardsDistribution.MIGHT_CARDS
                .Select(x => new MightCard
                {
                    Type = x.Type,
                    Value = x.Value,
                    IsCritical = x.IsCritical,
                    DeckId = enemyDeck.Id
                })
                .ToList();
            enemyMightCards.Shuffle();
            _mightCards.AddBatch(enemyMightCards);

            var freeCompany = new FreeCompany() { Name = "Test Free Company" };
            _freeCompanies.Add(freeCompany);

            foreach (var index in Enumerable.Range(0, 4))
            {
                var player = DefaultPlayers.Players[index];
                player.FreeCompanyId = freeCompany.Id;
                _players.Add(player);
                var encounterPlayer = DefaultPlayers.EncounterPlayers[index];
                encounterPlayer.EncounterId = encounter.Id;
                encounterPlayer.PlayerId = player.Id;
                _encounterPlayers.Add(encounterPlayer);
            }

            _bossFactory.CreateBossByNumber(1, encounter.Id);

            return encounter.Id;
        }
    }
}
