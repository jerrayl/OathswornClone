namespace RAZE.Entities
{
    public class PlayerBonus : BaseEntity
    {
        public int BonusTypeId { get; set; }

        public int PlayerId { get; set; }

        public int Number { get; set; }

        public virtual BonusType BonusType { get; set; }

        public virtual PlayerSession Player { get; set; }
    }
}
