using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Extensions;
using Oathsworn.Business.Constants;

namespace Oathsworn.Business.Services
{
    public interface IMightCardsService
    {
        List<MightCard> DrawCardsFromCritCards(int deckId, int attackId, List<MightCard> cards);
        List<MightCard> DrawCardsFromCards(int deckId, int attackId, List<MightCard> cards);
        List<MightCard> DrawCards(int deckId, int attackId, Dictionary<Might, int> cardsToDraw);
    }

    public class MightCardsService : IMightCardsService
    {
        private readonly IDatabaseRepository<MightCard> _mightCards;

        public MightCardsService(
            IDatabaseRepository<MightCard> mightCards
        )
        {
            _mightCards = mightCards;
        }

        public List<MightCard> DrawCardsFromCritCards(int deckId, int attackId, List<MightCard> cards)
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

        public List<MightCard> DrawCardsFromCards(int deckId, int attackId, List<MightCard> cards)
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

        public List<MightCard> DrawCards(int deckId, int attackId, Dictionary<Might, int> cardsToDraw)
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
    }
}
