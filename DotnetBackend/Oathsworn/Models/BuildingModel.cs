using System.Collections.Generic;

namespace RAZE.Models
{
    public class BuildingModel
    {
        public string Name { get; set; }

        public string Identifier { get; set; }

        public string Image { get; set; }

        public int Tier { get; set; }

        public string Element { get; set; }

        public int Production { get; set; }

        public int Health { get; set; }

        public string Effect { get; set; }

        public virtual List<CostModel> Cost { get; set; }
    }
}