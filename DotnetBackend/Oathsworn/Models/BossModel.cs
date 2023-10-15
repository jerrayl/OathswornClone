using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class BossModel
    {
        public int Id { get; set; }
        public Dictionary<BossPart, int> Health { get; set; }
        public int Defence { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Dictionary<Might, int> Might { get; set; }
    }
}