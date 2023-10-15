using System;
using System.Collections.Generic;

namespace RAZE.Entities
{
    public class PlayerSession : BaseEntity
    {
        public int AccountId { get; set; }
        public bool IsCurrentPlayer { get; set; }

        public int? GameRoomId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
        public virtual Account Account { get; set; }

        public virtual List<PlayerTroop> PlayerTroops { get; set; }

        public virtual List<PlayerBuilding> PlayerBuildings { get; set; }

        public virtual List<PlayerResource> PlayerResources { get; set; }

        public virtual List<PlayerProduction> PlayerProductions { get; set; }

        public virtual List<PlayerBonus> PlayerBonuses { get; set; }

        public virtual GameRoom GameRoom { get; set; }
    }
}
