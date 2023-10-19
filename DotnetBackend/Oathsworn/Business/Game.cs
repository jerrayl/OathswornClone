using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Models;
using Oathsworn.Extensions;
using AutoMapper;
using System.Collections;

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

        public Game(
            IDatabaseRepository<Encounter> encounters,
            IDatabaseRepository<EncounterMightDeck> encounterMightDecks,
            IDatabaseRepository<MightCard> mightCards,
            IDatabaseRepository<FreeCompany> freeCompanies,
            IDatabaseRepository<Player> players,
            IDatabaseRepository<EncounterPlayer> encounterPlayers,
            IDatabaseRepository<Boss> bosses,
            IDatabaseRepository<Attack> attacks,
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
            _attacks = attacks;
            _mapper = mapper;
        }

        public void CreatePlayer(CreatePlayerModel moveModel)
        {
            throw new NotImplementedException();
        }

        public GameStateModel Move(int encounterId, MoveModel moveModel)
        {
            var player = _encounterPlayers.ReadOne(x => x.EncounterId == encounterId && x.PlayerId == moveModel.PlayerId);

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            if (GridHelper.IsValidPath(new List<IPosition>(){player}.Concat(moveModel.Positions).ToList()) && player.CurrentAnimus >= moveModel.Positions.Count)
            {
                player.CurrentAnimus -= moveModel.Positions.Count;
                var lastPosition = moveModel.Positions.Last();
                player.XPosition = lastPosition.XPosition;
                player.YPosition = lastPosition.YPosition;
                _encounterPlayers.Update(player);
            }

            return GetGameState(encounterId);
        }

        public void SpendToken(int encounterId, SpendTokenModel spendTokenModel)
        {
            var player = _encounterPlayers.ReadOne(x => x.EncounterId == encounterId && x.PlayerId == spendTokenModel.PlayerId);

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            if (spendTokenModel.Token == Token.Animus && player.Tokens[Token.Animus] > 0)
            {
                player.Tokens[Token.Animus] -= 1;
                player.CurrentAnimus += Constants.ANIMUS_TOKEN_VALUE;
                _encounterPlayers.Update(player);
            }

            if (spendTokenModel.Token == Token.Battleflow && player.Tokens[Token.Battleflow] > 0)
            {
                //Perform logic
            }
        }

        public GameStateModel CompleteAttack(int encounterId, int attackId)
        {
            var attack = _attacks.ReadOne(x => x.Id == attackId, x => x.MightCards, x => x.Player, x => x.Player.EncounterPlayer, x => x.Boss);

            if (attack is null)
            {
                throw new Exception("Attack not found");
            }

            if (attack.MightCards.Count(x => !x.IsDrawnFromCritical && x.Value == 0) > Constants.NUM_ZEROES_TO_MISS)
            {
                return GetGameState(encounterId);
            }

            attack.Player.EncounterPlayer.Tokens[Token.Empower] -= attack.EmpowerTokensUsed;
            attack.Player.EncounterPlayer.Tokens[Token.Redraw] -= attack.RerollTokensUsed;
            _encounterPlayers.Update(attack.Player.EncounterPlayer);

            // Assume attack targets boss
            attack.Boss.Health[attack.BossPart] -= (attack.MightCards.Sum(x => x.Value) + attack.BonusDamage) / attack.Boss.Defence;
            if (attack.Boss.Health[attack.BossPart] <= 0)
            {
                attack.Boss.Health[attack.BossPart] = 0;
                //Perform boss break action
            }
            _bosses.Update(attack.Boss);

            _attacks.Delete(attack);

            return GetGameState(encounterId);
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

            var cardsNotRedrawn = attack.MightCards
                .Where(x => !rerollModel.MightCards.Contains(x.Id))
                .ToList();

            var cardsToRedraw = attack.MightCards
                .Where(x => rerollModel.MightCards.Contains(x.Id))
                .ToList();

            var critCards = DrawCardsFromCritCards(cardsToRedraw.First().DeckId, attack.Id, cardsToRedraw);
            var nonCritCards = DrawCardsFromCards(cardsToRedraw.First().DeckId, attack.Id, cardsToRedraw.Where(x => !x.IsCritical).ToList());

            _mightCards.DeleteBatch(cardsToRedraw);

            var cardModels = cardsNotRedrawn
                .Concat(critCards)
                .Concat(nonCritCards)
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
            // Check that a valid number of might dice have been selected
            // Check that Player is able to empower might cards
            // Calculate bonus damage that would be done

            var attack = new Attack()
            {
                PlayerId = attackModel.PlayerId,
                // Temporarily hardcode boss as target
                BossId = attackModel.EnemyId,
                // Temporarily hardcode core as target
                BossPart = BossPart.Core,
                // Temporarily hardcode bonus damage as 0
                BonusDamage = 0,
                EmpowerTokensUsed = attackModel.EmpowerTokensUsed
            };
            _attacks.Add(attack);

            var playerMightDeck = _encounterMightDecks
                .ReadOne(x => x.EncounterId == encounterId && x.IsFreeCompanyDeck);

            var cardsDrawn = new List<MightCard>();
            var cardsDrawnFromCrit = new List<MightCard>();

            cardsDrawn.AddRange(DrawCards(playerMightDeck.Id, attack.Id, attackModel.Might));

            if (cardsDrawn.Any(x => x.IsCritical))
            {
                cardsDrawnFromCrit.AddRange(DrawCardsFromCritCards(playerMightDeck.Id, attack.Id, cardsDrawn));
            }

            while (cardsDrawnFromCrit.Any(x => x.IsCritical))
            {
                cardsDrawn.AddRange(cardsDrawnFromCrit);
                cardsDrawnFromCrit = DrawCardsFromCritCards(playerMightDeck.Id, attack.Id, cardsDrawnFromCrit);
            }
            cardsDrawn.AddRange(cardsDrawnFromCrit);

            var cardModels = cardsDrawn.Select(x => _mapper.Map<MightCard, MightCardModel>(x)).ToList();

            return new AttackResponseModel
            {
                AttackId = attack.Id,
                CardsDrawn = cardModels
            };
        }

        private List<MightCard> DrawCardsFromCritCards(int deckId, int attackId, List<MightCard> cards)
        {
            var cardsDrawn = DrawCardsFromCards(
                deckId,
                attackId,
                cards.Where(x => x.IsCritical).ToList()
            );
            cardsDrawn.ForEach(x => x.IsDrawnFromCritical = true);
            _mightCards.UpdateBatch(cardsDrawn);
            return cardsDrawn;
        }

        private List<MightCard> DrawCardsFromCards(int deckId, int attackId, List<MightCard> cards)
        {
            var cardsDrawn = DrawCards(
                deckId,
                attackId,
                cards
                    .GroupBy(x => x.Type)
                    .ToDictionary(x => x.Key, x => x.Count())
            );
            return cardsDrawn;
        }

        private List<MightCard> DrawCards(int deckId, int attackId, Dictionary<Might, int> cardsToDraw)
        {
            var cardsDrawn = new List<MightCard>();
            var mightDeckCards = GetMightCards(deckId);
            foreach (var might in Enum.GetValues<Might>())
            {
                if (!cardsToDraw.ContainsKey(might))
                {
                    continue;
                }
                if (mightDeckCards[might].Count > cardsToDraw[might])
                {
                    cardsDrawn.AddRange(mightDeckCards[might].Take(cardsToDraw[might]));
                }
                else
                {
                    var extraCardsNeeded = cardsToDraw[might] - mightDeckCards[might].Count;
                    cardsDrawn.AddRange(mightDeckCards[might]);
                    RefreshMightDeck(might, deckId);
                    mightDeckCards = GetMightCards(deckId);
                    cardsDrawn.AddRange(mightDeckCards[might].Take(extraCardsNeeded));
                }
            }

            cardsDrawn.ForEach(x => x.AttackId = attackId);
            _mightCards.UpdateBatch(cardsDrawn);
            return cardsDrawn;
        }

        private Dictionary<Might, List<MightCard>> GetMightCards(int deckId)
        {
            var mightCards = _mightCards
                .Read(x => x.DeckId == deckId && x.AttackId is null);
            return _mightCards
                .Read(x => x.DeckId == deckId && x.AttackId is null)
                .OrderBy(x => x.Id)
                .GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        private void RefreshMightDeck(Might might, int deckId)
        {
            var mightCards = MightCardsDistribution.MIGHT_CARDS
            .Where(x => x.Type == might)
            .Select(x =>
                new MightCard
                {
                    Type = x.Type,
                    Value = x.Value,
                    IsCritical = x.IsCritical,
                    DeckId = deckId
                }
            ).ToList();
            mightCards.Shuffle();
            _mightCards.AddBatch(mightCards);
        }

        public GameStateModel GetGameState(int encounterId)
        {
            var players = _encounterPlayers.Read(x => x.EncounterId == encounterId, x => x.Player);
            var boss = _bosses.ReadOne(x => x.EncounterId == encounterId);

            return new GameStateModel()
            {
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
                Tokens = new() { { Token.Animus, 0 }, { Token.Battleflow, 0 }, { Token.Defence, 0 }, { Token.Empower, 5 }, { Token.Redraw, 5 } }
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
