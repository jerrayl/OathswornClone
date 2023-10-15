using RAZE.Models;

namespace RAZE.Business
{
    public interface IPlayer
    {
        string Login(string email, string connectionId);
        RequestModel SendRequest(string connectionId, string email, out string errorMessage);
        GameRoomModel AcceptRequest(string requestIdentifier, out string errorMessage);
    }
}
