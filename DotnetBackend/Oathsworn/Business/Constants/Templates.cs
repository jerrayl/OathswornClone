using System.Collections.Generic;
using System.Linq;
using Oathsworn.Entities;
using Oathsworn.Models;

namespace Oathsworn.Business.Constants
{
    public static class Templates
    {
        public static IEnumerable<IPosition> GetTemplateForRing(int size)
        {
            if (size == 0)
            {
                return new List<IPosition>() {
                    new Position(){XPosition = 0, YPosition = 0}
                };
            }

            var positions = new List<IPosition>();

            foreach (var i in Enumerable.Range(0, size))
            {
                // Northeast side
                positions.Add(new Position() { XPosition = i, YPosition = size * -1 });
                // Southwest side
                positions.Add(new Position() { XPosition = i * -1, YPosition = size });
            }

            if (size > 1)
            {
                foreach (var i in Enumerable.Range(0, size - 1))
                {
                    // East side
                    positions.Add(new Position() { XPosition = size, YPosition = size * -1 });
                    // West side
                    positions.Add(new Position() { XPosition = size * -1, YPosition = i });
                }
            }

            if (size > 1)
            {
                foreach (var i in Enumerable.Range(1, size - 1))
                {
                    // Southeast side
                    positions.Add(new Position() { XPosition = size - i, YPosition = i });
                    // Northwest side
                    positions.Add(new Position() { XPosition = i - size, YPosition = i * -1 });
                }
            }

            return positions;
        }

        public static IEnumerable<IPosition> GetTemplateForHex(int size)
        {
            var positions = new List<IPosition>();

            foreach (var i in Enumerable.Range(0, size))
            {
                positions.Concat(GetTemplateForRing(i));
            }

            return positions;
        }
    }
}