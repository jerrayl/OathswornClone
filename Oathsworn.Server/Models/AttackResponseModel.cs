using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class AttackResponseModel
    {
        public int AttackId { get; set; }
        public List<MightCardModel> CardsDrawn { get; set; }
    }
}