using RAZE.Models;

namespace RAZE.Business
{
    public interface IGame
    {
        void InitializeGame(string gameRoomIdentifier);
        GameStateModel GetGameState (string gameRoomIdentifier);
        string PlaceBuilding(string connectionId, string buildingIdentifier, int boardSpace, out string errorMessage);
    }
}
