using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class RerollModel
    {
        public int AttackId { get; set; }
        public List<int> MightCards { get; set; }
        public int RerollTokensUsed { get; set; }
    }
}