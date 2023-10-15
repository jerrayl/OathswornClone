namespace Oathsworn.Entities
{
    public class MightCard : BaseEntity
    {
        public int DeckId { get; set; }
        public int? AttackId { get; set; }
        public int Value { get; set; }
        public Might Type { get; set; }
        public bool IsCritical { get; set; }

        public virtual EncounterMightDeck EncounterMightDeck { get; set; }
    }
}
