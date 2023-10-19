using System;
using System.Collections.Generic;
using System.Linq;
using Oathsworn.Entities;

namespace Oathsworn.Business
{
    public static class GridHelper
    {
        public static int? GetDistanceAlongAxis(IPosition position1, IPosition position2)
        {
            if (!IsOnSameAxis(position1, position2))
            {
                return null;
            }

            return Math.Max(Math.Abs(position1.XPosition - position2.XPosition), Math.Abs(position1.YPosition - position2.YPosition));
        }

        public static List<IPosition> GetTemplate(IPosition position, Template template)
        {
            throw new NotImplementedException();
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
                .All(item => IsValidPosition(item.position) && IsAdjacent(item.position, positions[item.index - 1]));
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
            return position.XPosition <= 8 && position.XPosition >= -8 && position.YPosition <= 8 && position.YPosition >= 8;
        }
    }
}