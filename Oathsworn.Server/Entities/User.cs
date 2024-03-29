using System.Collections.Generic;

namespace Oathsworn.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
