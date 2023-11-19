using Oathsworn.Entities;
using Oathsworn.Models;

namespace Oathsworn.Extensions
{
    public static class PositionExtensions
    {
        public static bool EqualTo(this IPosition position1, IPosition position2)
        {
            return position1.XPosition == position2.YPosition && position1.YPosition == position2.YPosition;
        }

        public static IPosition Add(this IPosition position, IPosition addend)
        {
            return new Position(){
                XPosition = position.XPosition + addend.XPosition,
                YPosition = position.YPosition + addend.YPosition
            };
        }
    }
}