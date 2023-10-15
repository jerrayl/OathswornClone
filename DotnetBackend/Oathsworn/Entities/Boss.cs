using System.Collections.Generic;

namespace Oathsworn.Entities
{
    public class Boss : BaseEntity
    {
        public int EncounterId { get; set; }
        public Dictionary<BossPosition, int> Health { get; set; }
        public int Defence { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Dictionary<Might, int> Might { get; set; }

        public virtual Encounter Encounter { get; set; }
        public virtual List<BossAction> BossActions { get; set; }
    }
}
