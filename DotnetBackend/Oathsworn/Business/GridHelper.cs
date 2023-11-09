using System;
using System.Collections.Generic;
using System.Linq;
using Oathsworn.Entities;
using Oathsworn.Extensions;

namespace Oathsworn.Business
{
    public static class GridHelper
    {
        public const int COORDINATE_MAX_VALUE = 8;
        public const int COORDINATE_MIN_VALUE = COORDINATE_MAX_VALUE * -1;

        public static int? GetDistanceAlongAxis(IPosition position1, IPosition position2)
        {
            if (!IsOnSameAxis(position1, position2))
            {
                return null;
            }

            return Math.Max(Math.Abs(position1.XPosition - position2.XPosition), Math.Abs(position1.YPosition - position2.YPosition));
        }

        public static List<IPosition> GetTemplate(IPosition position, Template template, int size = 1, Direction? direction = null, Corner? corner = null)
        {
            if (direction != null && corner != null)
            {
                throw new ArgumentException();
            }

            var templatePositions = template switch
            {
                Template.Cone => throw new NotImplementedException(),
                Template.Wave => throw new NotImplementedException(),
                Template.Hex => Templates.GetTemplateForHex(size),
                _ => throw new NotImplementedException()
            };

            return templatePositions.Select(x => x.Add(position)).Where(IsValidPosition).ToList();
        }

        public static bool IsOnSameAxis(IPosition position1, IPosition position2)
        {
            return
                (position1.XPosition == position2.XPosition) ||
                position1.YPosition == position2.YPosition ||
                Math.Abs(position1.XPosition - position2.XPosition) == Math.Abs(position1.YPosition - position2.YPosition) && (position1.XPosition - position2.XPosition) * (position1.YPosition - position2.YPosition) < 0;
        }

        public static bool IsValidPath(List<IPosition> positions)
        {
            if (positions.Count < 2)
            {
                return false;
            }

            return IsValidPosition(positions.First()) && positions
                .Skip(1)
                .Select((position, index) => new { index, position })
                .All(item => IsValidPosition(item.position) && IsAdjacent(item.position, positions[item.index]));
        }

        public static bool IsAdjacent(IPosition position1, IPosition position2)
        {
            return
                (position1.XPosition == position2.XPosition && Math.Abs(position1.YPosition - position2.YPosition) == 1) ||
                (position1.YPosition == position2.YPosition && Math.Abs(position1.XPosition - position2.XPosition) == 1) ||
                (position1.XPosition - position2.XPosition) * (position1.YPosition - position2.YPosition) == -1;
        }

        public static bool IsValidPosition(IPosition position)
        {
            return position.XPosition <= COORDINATE_MAX_VALUE && position.XPosition >= COORDINATE_MIN_VALUE && position.YPosition <= COORDINATE_MAX_VALUE && position.YPosition >= COORDINATE_MIN_VALUE;
        }

        // Overaching game rule preferences north and west to break ties, hence this direction is hardcoded 
        public static IPosition GetNorthWestiest(List<IPosition> positions)
        {
            var getNorthValue = (IPosition p) => p.XPosition + 2 * p.YPosition;
            return positions.Min(Comparer<IPosition>.Create((a, b) =>
            {
                var northValueDifference = getNorthValue(a) - getNorthValue(b);
                return northValueDifference == 0 ? a.XPosition- b.XPosition : northValueDifference;
            }));
        }
    }
}