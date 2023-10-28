using System;
using System.Collections.Generic;
using System.Linq;

namespace Oathsworn.Business
{
    public static class CardsHelper
    {
        private static List<int> GetMightPoints(Dictionary<Might, int> might){
            return Enumerable.Repeat(Constants.BLACK_MIGHT_VALUE, might[Might.Black])
                .Concat(Enumerable.Repeat(Constants.RED_MIGHT_VALUE, might[Might.Red]))
                .Concat(Enumerable.Repeat(Constants.YELLOW_MIGHT_VALUE, might[Might.Yellow]))
                .ToList();
        }

        public static int GetEmpowerTokensNeeded(Dictionary<Might, int> mightCards, Dictionary<Might, int> playerMight)
        {
            var playerMightPoints = GetMightPoints(playerMight);
            var mightPointsRequired = GetMightPoints(mightCards).Select((mightPoints, i) => 
                i < Constants.MAXIMUM_PLAYER_MIGHT - 1 && playerMightPoints.Count > i && mightPoints >= playerMightPoints[i] ? 
                mightPoints - playerMightPoints[i] : 
                mightPoints
            ).Sum();


            return (int)Math.Ceiling(decimal.Divide(mightPointsRequired,Constants.EMPOWER_TOKEN_VALUE));
        }
    }
}