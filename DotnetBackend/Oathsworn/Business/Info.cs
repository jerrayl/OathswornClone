using System.Collections.Generic;
using System.Linq;
using RAZE.Entities;
using RAZE.Models;
using RAZE.Repositories;
using AutoMapper;

namespace RAZE.Business
{
    public class Info : IInfo
    {
        private IDatabaseRepository<Building> _buildings;
        private IDatabaseRepository<BuildingCost> _buildingCosts;
        private IDatabaseRepository<Troop> _troops;
        private IDatabaseRepository<TroopCost> _troopCosts;
        private IDatabaseRepository<Element> _elements;
        private IMapper _mapper;

        public Info(
            IDatabaseRepository<Building> buildings,
            IDatabaseRepository<BuildingCost> buildingCosts,
            IDatabaseRepository<Troop> troops,
            IDatabaseRepository<TroopCost> troopCosts,
            IDatabaseRepository<Element> elements,
            IMapper mapper)
        {
            _buildings = buildings;
            _buildingCosts = buildingCosts;
            _troops = troops;
            _elements = elements;
            _mapper = mapper;
        }

        public List<BuildingModel> GetBuildings()
        {
            var elements = _elements.Read().ToDictionary(element => element.Id, element => element.Name);

            var buildings = _buildings.Read(building => true, building => building.BuildingCosts, building => building.Element);
            return
                buildings
                .Select(building =>
                {
                    var newBuilding = _mapper.Map<Building, BuildingModel>(building);
                    newBuilding.Cost = building.BuildingCosts.Select(cost => new CostModel()
                    {
                        Element = elements[cost.ElementId],
                        Number = cost.Number
                    }).ToList();
                    return newBuilding;
                })
                .ToList();
        }

        public List<TroopModel> GetTroops()
        {
            return new List<TroopModel>();
        }
    }
}
