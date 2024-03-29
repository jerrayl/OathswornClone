namespace Oathsworn.Models
{
    public class CreatePlayerModel
    {
        public string Name { get; set; }
        public Class Class { get; set; }
    }

    public class PlayerSummaryModel : CreatePlayerModel
    {
        public int Id { get; set; }
    }
}