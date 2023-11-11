using System.Collections.Generic;

namespace Oathsworn.Entities
{
    public class BossAttack : BaseEntity
    {
        public int BossId { get; set; }

        public virtual List<Player> Players { get; set; }
        public virtual Boss Boss { get; set; }
        public virtual List<MightCard> MightCards { get; set; }
    }
}
