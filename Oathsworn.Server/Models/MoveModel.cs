using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class MoveModel
    {
        public int PlayerId { get; set; }
        public List<Position> Positions { get; set; }
    }
}