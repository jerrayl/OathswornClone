namespace Oathsworn.Models
{
    public class MightCardModel
    {
        public int Value { get; set; }
        public Might Type { get; set; }
        public bool IsCritical { get; set; }
        public bool IsDrawnFromCritical { get; set; }
    }
}