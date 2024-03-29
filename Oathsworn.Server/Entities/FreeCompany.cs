using System.Collections.Generic;

namespace Oathsworn.Entities
{
    public class FreeCompany : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
