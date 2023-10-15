namespace RAZE.Entities
{
    public class GameRoom : BaseEntity
    {
        public string Identifier { get; set; }

        public int? StatusTypeId { get; set; }

        public virtual StatusType StatusType { get; set; }
    }
}
