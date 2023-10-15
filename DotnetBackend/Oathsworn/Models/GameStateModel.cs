using System.Collections.Generic;

namespace RAZE.Models
{
    public class GameStateModel
    {
        public string Status { get; set; }
        public string CurrentPlayer { get; set; }
        public List<PlayerModel> Players { get; set; }
    }

    public class PlayerModel
    {
        public string Email { get; set; }
        public List<PlayerBonusModel> PlayerBonuses { get; set; }
        public List<PlayerResourceModel> PlayerResources { get; set; }
        public List<PlayerResourceModel> PlayerProduction { get; set; }
        public List<PlayerBuildingModel> PlayerBuildings { get; set; }
    }

    public class PlayerResourceModel
    {
        public string Element { get; set; }
        public int Number { get; set; }
    }

    public class PlayerBonusModel
    {
        public string BonusType { get; set; }
        public int Number { get; set; }
    }

    public class PlayerBuildingModel
    {
        public string BuildingIdentifier { get; set; }
        public int BoardSpace { get; set; }
        public int Health { get; set; }
    }
}