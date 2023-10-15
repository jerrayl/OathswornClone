namespace RAZE.Entities
{
    public class BuildingCost : BaseEntity
    {
        public int BuildingId { get; set; }

        public int ElementId { get; set; }

        public int Number { get; set; }
    }
}
