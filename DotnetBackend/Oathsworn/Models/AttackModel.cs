using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class AttackModel
    {
        public Dictionary<Might, int> Might { get; set; }
        public int EmpowerTokensUsed { get; set; }
        public int PlayerId { get; set; }
        public int EnemyId { get; set; }
        public bool IsBossTargeted { get; set; }
    }
}