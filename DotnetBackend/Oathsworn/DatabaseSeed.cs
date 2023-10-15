using System;
using System.Collections.Generic;
using System.Linq;
using RAZE.Entities;

namespace RAZE.Database
{
    public class DatabaseSeed
    {
        public static void Seed(DatabaseContext database)
        {
            var elements = new List<Element>(){
                new Element() { Name = Constants.FIRE},
                new Element() { Name = Constants.WATER},
                new Element() { Name = Constants.EARTH},
                new Element() { Name = Constants.AIR},
                new Element() { Name = Constants.FOOD},
                new Element() { Name = Constants.ARCANA}
            };

            foreach (var element in elements)
            {
                if (!database.Elements.Any(e => e.Name == element.Name))
                    database.Add(element);
            }

            database.SaveChanges();

            var elementsDict = database.Elements.ToDictionary(e => e.Name, e => e.Id);

            var bonusTypes = new List<BonusType>(){
                new BonusType() {Name = Constants.ATTACK},
                new BonusType() {Name = Constants.TROOP_HEALTH},
                new BonusType() {Name = Constants.BUILDING_HEALTH},
                new BonusType() {Name = Constants.FOOD}
            };

            foreach (var bonusType in bonusTypes)
            {
                if (!database.BonusTypes.Any(b => b.Name == bonusType.Name))
                    database.Add(bonusType);
            }

            var statusTypes = new List<StatusType>(){
                new StatusType() {Name = Constants.BUILD},
                new StatusType() {Name = Constants.COMBAT},
                new StatusType() {Name = Constants.GAME_OVER}
            };

            foreach (var statusType in statusTypes)
            {
                if (!database.StatusTypes.Any(s => s.Name == statusType.Name))
                    database.Add(statusType);
            }


            var buildings = new List<Building>(){
                new Building()
                {
                    Name = "Bakery",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Image = "FOOD-1",
                    Tier = 1,
                    ElementId = elementsDict[Constants.FOOD],
                    Production = 1,
                    Health = 1
                },
                new Building(){
                    Name = "Farm",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Image = "FOOD-2",
                    Tier = 2,
                    ElementId = elementsDict[Constants.FOOD],
                    Production = 2,
                    Health = 2
                },
                new Building(){
                    Name = "Tavern",
                    Image = "FOOD-3",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.FOOD],
                    Production = 3,
                    Health = 3
                },
                new Building(){
                    Name = "Sawmill",
                    Image = "FIRE-1",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 1,
                    ElementId = elementsDict[Constants.FIRE],
                    Production = 1,
                    Health = 1
                },
                new Building(){
                    Name = "Oilfield",
                    Image = "FIRE-2",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 2,
                    ElementId = elementsDict[Constants.FIRE],
                    Production = 2,
                    Health = 2
                },
                new Building(){
                    Name = "Magic Forge",
                    Image = "FIRE-3",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.FIRE],
                    Production = 3,
                    Health = 3,
                    Effect = "With 2, Troops +1 Attack"
                },
                new Building()
                    {
                    Name = "Well",
                    Image = "WATER-1",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 1,
                    ElementId = elementsDict[Constants.WATER],
                    Production = 1,
                    Health = 1
                },
                new Building()
                    {
                    Name = "Potion Lab",
                    Image = "WATER-2",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 2,
                    ElementId = elementsDict[Constants.WATER],
                    Production = 2,
                    Health = 2
                },
                new Building(){
                    Name = "Distillation Plant",
                    Image = "WATER-3",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.WATER],
                    Production = 3,
                    Health = 3,
                    Effect = "With 2, Troops +1 Health"
                },
                new Building(){
                    Name = "Mine",
                    Image = "EARTH-1",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 1,
                    ElementId = elementsDict[Constants.EARTH],
                    Production = 1,
                    Health = 1
                },
                new Building(){
                    Name = "Quarry",
                    Image = "EARTH-2",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 2,
                    ElementId = elementsDict[Constants.EARTH],
                    Production = 2,
                    Health = 2
                },
                new Building(){
                    Name = "Occult Crystal",
                    Image = "EARTH-3",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.EARTH],
                    Production = 3,
                    Health = 3,
                    Effect = "Each 4, Building Health +1"
                },
                new Building(){
                    Name = "Windmill",
                    Image = "AIR-1",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 1,
                    ElementId = elementsDict[Constants.AIR],
                    Production = 1,
                    Health = 1
                },
                new Building(){
                    Name = "Airship Tower",
                    Image = "AIR-2",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 2,
                    ElementId = elementsDict[Constants.AIR],
                    Production = 2,
                    Health = 2
                },
                new Building(){
                    Name = "Meteorology Tower",
                    Image = "AIR-3",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.AIR],
                    Production = 3,
                    Health = 3,
                    Effect = "Each 3, Food Production +1"
                },
                new Building(){
                    Name = "Castle",
                    Image = "CASTLE",
                    Identifier = Guid.NewGuid().ToString().ToLower(),
                    Tier = 3,
                    ElementId = elementsDict[Constants.ARCANA],
                    Production = 5,
                    Health = 5,
                    Effect= "Limited to 1, lose game when castle is destroyed"
                }
            };

            foreach (var building in buildings)
            {
                if (!database.Buildings.Any(b => b.Name == building.Name))
                    database.Add(building);
            }

            database.SaveChanges();

            var buildingsDict = database.Buildings.ToDictionary(e => e.Name, e => e.Id);

            var buildingCosts = new List<BuildingCost>(){
                new BuildingCost(){BuildingId = buildingsDict["Bakery"], ElementId = elementsDict[Constants.FIRE], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Bakery"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Farm"], ElementId = elementsDict[Constants.WATER], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Farm"], ElementId = elementsDict[Constants.EARTH], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Tavern"], ElementId = elementsDict[Constants.FIRE], Number = 4},
                new BuildingCost(){BuildingId = buildingsDict["Tavern"], ElementId = elementsDict[Constants.WATER], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Tavern"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Sawmill"], ElementId = elementsDict[Constants.WATER], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Sawmill"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Sawmill"], ElementId = elementsDict[Constants.AIR], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Oilfield"], ElementId = elementsDict[Constants.WATER], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Oilfield"], ElementId = elementsDict[Constants.EARTH], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Oilfield"], ElementId = elementsDict[Constants.AIR], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Magic Forge"], ElementId = elementsDict[Constants.FIRE], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Magic Forge"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Magic Forge"], ElementId = elementsDict[Constants.ARCANA], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Well"], ElementId = elementsDict[Constants.WATER], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Well"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Potion Lab"], ElementId = elementsDict[Constants.FIRE], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Potion Lab"], ElementId = elementsDict[Constants.WATER], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Distillation Plant"], ElementId = elementsDict[Constants.WATER], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Distillation Plant"], ElementId = elementsDict[Constants.AIR], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Distillation Plant"], ElementId = elementsDict[Constants.ARCANA], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Mine"], ElementId = elementsDict[Constants.EARTH], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Mine"], ElementId = elementsDict[Constants.AIR], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Quarry"], ElementId = elementsDict[Constants.WATER], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Quarry"], ElementId = elementsDict[Constants.EARTH], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Quarry"], ElementId = elementsDict[Constants.AIR], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Occult Crystal"], ElementId = elementsDict[Constants.FIRE], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Occult Crystal"], ElementId = elementsDict[Constants.EARTH], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Occult Crystal"], ElementId = elementsDict[Constants.ARCANA], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Windmill"], ElementId = elementsDict[Constants.EARTH], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Windmill"], ElementId = elementsDict[Constants.AIR], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Airship Tower"], ElementId = elementsDict[Constants.FIRE], Number = 2},
                new BuildingCost(){BuildingId = buildingsDict["Airship Tower"], ElementId = elementsDict[Constants.AIR], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Meteorology Tower"], ElementId = elementsDict[Constants.WATER], Number = 1},
                new BuildingCost(){BuildingId = buildingsDict["Meteorology Tower"], ElementId = elementsDict[Constants.AIR], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Meteorology Tower"], ElementId = elementsDict[Constants.ARCANA], Number = 3},
                new BuildingCost(){BuildingId = buildingsDict["Castle"], ElementId = elementsDict[Constants.ARCANA], Number = 5}
            };

            foreach (var buildingCost in buildingCosts)
            {
                if (!database.BuildingCosts.Any(bc => bc.BuildingId == buildingCost.BuildingId && bc.ElementId == buildingCost.ElementId))
                    database.Add(buildingCost);
            }

            database.SaveChanges();
        }
    }
}