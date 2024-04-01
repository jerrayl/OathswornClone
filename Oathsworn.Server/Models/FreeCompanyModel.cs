using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class FreeCompanyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<PlayerSummaryModel> Players { get; set; }
    }
}