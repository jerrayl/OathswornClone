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
        Direction Direction { get; set; }
        Corner Corner { get; set; }
    }
}