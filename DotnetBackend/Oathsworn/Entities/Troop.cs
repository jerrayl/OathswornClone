using System.Collections.Generic;

namespace RAZE.Entities
{
    public class Troop : BaseEntity
    {
        public string Name { get; set; }

        public string Identifier { get; set; }

        public string Image { get; set; }

        public int Tier { get; set; }

        public int ElementId { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public virtual Element Element { get; set; }

        public virtual List<TroopCost> TroopCosts { get; set; }
    }
}
