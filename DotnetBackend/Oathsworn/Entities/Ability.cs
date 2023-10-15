namespace Oathsworn.Entities
{
    public class Ability : BaseEntity
    {
        public Class AbilityClass { get; set; }
        public int Animus { get; set; }
        public int Battleflow { get; set; }
        public string Effects { get; set; }
    }
}
