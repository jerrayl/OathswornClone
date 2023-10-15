using System.Collections.Generic;

namespace Oathsworn.Models
{
    public class GameStateModel
    {
        public List<PlayerModel> Players { get; set; }
        public BossModel Boss { get; set; }
    }
}