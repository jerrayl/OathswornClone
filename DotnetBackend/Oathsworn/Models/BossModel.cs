using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class BossModel
    {
        public int Id { get; set; }
        public Dictionary<string, int> Health { get; set; }
        public int Defence { get; set; }
        public List<BorderedPosition> Positions { get; set; }
        public Dictionary<Might, int> Might { get; set; }
    }
}