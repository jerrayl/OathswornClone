using System.Collections.Generic;

namespace Oathsworn.Entities
{
    public class Boss : BaseEntity, IPosition
    {
        public int EncounterId { get; set; }
        public Dictionary<string, int> Health { get; set; }
        public int Defence { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Dictionary<Might, int> Might { get; set; }
        public Direction Direction { get; set; }

        public virtual Encounter Encounter { get; set; }
        public virtual List<BossAction> BossActions { get; set; }
    }
}
