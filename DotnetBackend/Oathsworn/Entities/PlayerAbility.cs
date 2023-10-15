namespace Oathsworn.Entities
{
    public class PlayerAbility : BaseEntity
    {
        public int PlayerId { get; set; }
        public int AbilityId { get; set; }
        public int? Battleflow { get; set; }

        public virtual Ability Ability { get; set; }
    }
}
