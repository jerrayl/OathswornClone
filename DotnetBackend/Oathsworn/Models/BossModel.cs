using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class BossModel
    {
        public int Id { get; set; }
        public Dictionary<string, int> Health { get; set; }
        public int Defence { get; set; }
        public Position MainPosition { get; set; }
        public List<BorderedPosition> SecondaryPositions { get; set; }
        public Dictionary<Might, int> Might { get; set; }
    }
}