using System;
using RAZE.Entities;
using RAZE.Repositories;
using RAZE.Models;

namespace RAZE.Business
{
    public class Player : IPlayer
    {
        private IDatabaseRepository<PlayerSession> _playerSessions;
        private IDatabaseRepository<Account> _accounts;
        private IDatabaseRepository<Request> _requests;
        private IDatabaseRepository<GameRoom> _gameRooms;

        public Player(
            IDatabaseRepository<PlayerSession> playerSessions,
            IDatabaseRepository<Account> accounts,
            IDatabaseRepository<Request> request,
            IDatabaseRepository<GameRoom> gameRooms)
        {
            _playerSessions = playerSessions;
            _accounts = accounts;
            _requests = request;
            _gameRooms = gameRooms;
        }

        public string Login(string email, string connectionId)
        {
            var userAccount = _accounts.ReadOne(account => account.Email.Equals(email));

            if (userAccount is null)
            {
                userAccount = new Account() { Email = email };
                _accounts.Add(userAccount);
            }

            var activeSession = _playerSessions.ReadOne(
                session => session.AccountId.Equals(userAccount.Id) && session.ExpiresAt > DateTime.UtcNow, 
                session => session.GameRoom);

            if (activeSession is null){
                activeSession = new PlayerSession() {
                    AccountId = userAccount.Id, 
                    ExpiresAt = DateTime.UtcNow.AddHours(2), 
                    Token = connectionId
                };
                _playerSessions.Add(activeSession);
            } else {
                activeSession.Token = connectionId;
                _playerSessions.Update(activeSession);
            }

            return activeSession.GameRoom?.Identifier;
        }

        public RequestModel SendRequest(string connectionId, string email, out string errorMessage){
            errorMessage = null;

            var senderSession = _playerSessions.ReadOne(session => session.Token.Equals(connectionId) && session.ExpiresAt > DateTime.UtcNow, senderSession => senderSession.Account);

            if (senderSession is null){
                errorMessage = "Player is not logged in";
                return null;
            }

            var receiverAccount = _accounts.ReadOne(account => account.Email.Equals(email));

            if (receiverAccount is null){
                errorMessage = "Email could not be found";
                return null;
            }

            var receiverSession = _playerSessions.ReadOne(session => session.AccountId.Equals(receiverAccount.Id) && session.ExpiresAt > DateTime.UtcNow);

            if (receiverSession is null){
                errorMessage = "Receiver is not logged in";
                return null;
            }

            var request = new Request() {
                SenderId = senderSession.AccountId, 
                ReceiverId = receiverAccount.Id, 
                ExpiresAt = DateTime.UtcNow.AddHours(1), 
                Identifier = Guid.NewGuid().ToString().ToLower()
            };

            _requests.Add(request);

            return new RequestModel(){
                Sender = senderSession.Account.Email, 
                ReceiverConnectionId = receiverSession.Token, 
                ExpiresAt = request.ExpiresAt, 
                RequestIdentifier = request.Identifier
            };
        }

        public GameRoomModel  AcceptRequest(string requestIdentifier, out string errorMessage){
            errorMessage = null;

            var request = _requests.ReadOne(request => request.Identifier.Equals(requestIdentifier));

            if (request.ExpiresAt < DateTime.UtcNow){
                errorMessage = "Request has expired";
                return null;
            }

            var gameRoom =  new GameRoom(){
                Identifier = Guid.NewGuid().ToString().ToLower()
            };

            _gameRooms.Add(gameRoom);

            var sender = _playerSessions.ReadOne(player => player.Id == request.SenderId);
            sender.GameRoomId = gameRoom.Id;
            _playerSessions.Update(sender);
            var receiver = _playerSessions.ReadOne(player => player.Id == request.ReceiverId);
            receiver.GameRoomId = gameRoom.Id;
            _playerSessions.Update(receiver);

            return new GameRoomModel(){SenderConnectionId = sender.Token, ReceiverConnectionId = receiver.Token, RoomIdentifier = gameRoom.Identifier};
        }
    }
}
