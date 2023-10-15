namespace RAZE.Entities
{
    public class PlayerResource : BaseEntity
    {
        public int ElementId { get; set; }

        public int PlayerId { get; set; }

        public int Number { get; set; }

        public virtual Element Element { get; set; }

        public virtual PlayerSession Player { get; set; }
    }
}
