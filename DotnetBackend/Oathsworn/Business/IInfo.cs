using RAZE.Models;
using System.Collections.Generic;
namespace RAZE.Business
{
    public interface IInfo
    {
        List<BuildingModel> GetBuildings();
        List<TroopModel> GetTroops();
    }
}
