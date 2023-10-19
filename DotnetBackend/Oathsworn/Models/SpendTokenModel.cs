namespace Oathsworn.Models
{
    public class SpendTokenModel
    {
        public int PlayerId { get; set; }
        public Token Token { get; set; }
        public int? BattleflowNumber { get; set; }
    }
}