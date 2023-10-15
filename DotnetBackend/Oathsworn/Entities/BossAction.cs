using System;

namespace Oathsworn.Entities
{
    public class BossAction : BaseEntity
    {
        public int BossId { get; set; }
        public int ActionId { get; set; }

        public virtual Boss Boss { get; set; }
        public virtual Action Action { get; set; }
    }
}
