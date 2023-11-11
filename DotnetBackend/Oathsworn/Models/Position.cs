using Oathsworn.Entities;

namespace Oathsworn.Models
{
    public class Position : IPosition
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }

    public class BorderedPosition : Position
    {
        public Direction? Direction { get; set; }
        public Corner? Corner { get; set; }
    }
}