using System;
using System.Linq;
using System.Collections.Generic;
using RAZE.Entities;
using RAZE.Repositories;
using RAZE.Models;

namespace RAZE.Business
{
    public class Game : IGame
    {
        private IDatabaseRepository<GameRoom> _gameRooms;
        private IDatabaseRepository<PlayerSession> _playerSessions;
        private IDatabaseRepository<PlayerBonus> _playerBonuses;
        private IDatabaseRepository<PlayerResource> _playerResources;
        private IDatabaseRepository<PlayerProduction> _playerProduction;
        private IDatabaseRepository<PlayerBuilding> _playerBuildings;
        private IDatabaseRepository<StatusType> _statusTypes;
        private IDatabaseRepository<Building> _buildings;
        private IDatabaseRepository<BonusType> _bonusTypes;
        private IDatabaseRepository<Element> _elements;
        private Dictionary<int, string> _buildingNameCache = null;
        private Dictionary<int, string> _bonusTypeNameCache = null;
        private Dictionary<int, string> _elementNameCache = null;
        private Dictionary<string, int> _elementIdCache = null;
        private Dictionary<string, int> _bonusTypeIdCache = null;
        private Dictionary<string, int> _statusTypeIdCache = null;
        private readonly List<string> BASE_ELEMENTS = new List<string> { Constants.FIRE, Constants.WATER, Constants.EARTH, Constants.AIR };

        public Game(
            IDatabaseRepository<GameRoom> gameRooms,
            IDatabaseRepository<PlayerSession> playerSessions,
            IDatabaseRepository<PlayerBonus> playerBonuses,
            IDatabaseRepository<PlayerResource> playerResources,
            IDatabaseRepository<PlayerProduction> playerProduction,
            IDatabaseRepository<PlayerBuilding> playerBuildings,
            IDatabaseRepository<StatusType> statusType,
            IDatabaseRepository<Building> buildings,
            IDatabaseRepository<BonusType> bonusTypes,
            IDatabaseRepository<Element> elements)
        {
            _gameRooms = gameRooms;
            _playerSessions = playerSessions;
            _playerBonuses = playerBonuses;
            _playerResources = playerResources;
            _playerProduction = playerProduction;
            _playerBuildings = playerBuildings;
            _statusTypes = statusType;
            _buildings = buildings;
            _bonusTypes = bonusTypes;
            _buildings = buildings;
            _elements = elements;
        }

        private void CreateCache()
        {
            if (_buildingNameCache is null)
                _buildingNameCache = _buildings.Read().ToDictionary(building => building.Id, building => building.Identifier);
            if (_bonusTypeNameCache is null)
                _bonusTypeNameCache = _bonusTypes.Read().ToDictionary(bonusType => bonusType.Id, bonusType => bonusType.Name);
            if (_elementNameCache is null)
                _elementNameCache = _elements.Read().ToDictionary(element => element.Id, element => element.Name);
            if (_elementIdCache is null)
                _elementIdCache = _elements.Read().ToDictionary(element => element.Name, element => element.Id);
            if (_bonusTypeIdCache is null)
                _bonusTypeIdCache = _bonusTypes.Read().ToDictionary(bonusType => bonusType.Name, bonusType => bonusType.Id);
            if (_statusTypeIdCache is null)
                _statusTypeIdCache = _statusTypes.Read().ToDictionary(statusType => statusType.Name, statusType => statusType.Id);
        }

        public GameStateModel GetGameState(string gameRoomIdentifier)
        {
            CreateCache();
            var gameRoom = _gameRooms.ReadOne(gameRoom => gameRoom.Identifier == gameRoomIdentifier, gameRoom => gameRoom.StatusType);
            var gameState = new GameStateModel()
            {
                Status = gameRoom.StatusType.Name,
                Players = new List<PlayerModel>()
            };

            var players = _playerSessions.Read(player => player.GameRoomId.Equals(gameRoom.Id),
                player => player.PlayerBonuses,
                player => player.PlayerResources,
                player => player.PlayerProductions,
                player => player.PlayerBuildings,
                player => player.Account);

            gameState.CurrentPlayer = players.Where(player => player.IsCurrentPlayer).Single().Account.Email;

            foreach (var player in players)
            {
                var playerModel = new PlayerModel() { Email = player.Account.Email };
                playerModel.PlayerBonuses = player.PlayerBonuses.Select(bonus => new PlayerBonusModel() { BonusType = _bonusTypeNameCache[bonus.BonusTypeId], Number = bonus.Number }).ToList();
                playerModel.PlayerResources = player.PlayerResources.Select(resource => new PlayerResourceModel() { Element = _elementNameCache[resource.ElementId], Number = resource.Number }).ToList();
                playerModel.PlayerProduction = player.PlayerProductions.Select(production => new PlayerResourceModel() { Element = _elementNameCache[production.ElementId], Number = production.Number }).ToList();
                playerModel.PlayerBuildings = player.PlayerBuildings.Select(building => new PlayerBuildingModel() { BoardSpace = building.BoardSpace, BuildingIdentifier = _buildingNameCache[building.BuildingId] }).ToList();
                gameState.Players.Add(playerModel);
            }

            return gameState;
        }

        public void InitializeGame(string gameRoomIdentifier)
        {
            CreateCache();
            var gameRoom = _gameRooms.ReadOne(gameRoom => gameRoom.Identifier == gameRoomIdentifier);
            var players = _playerSessions.Read(player => player.GameRoomId.Equals(gameRoom.Id));

            gameRoom.StatusTypeId = _statusTypeIdCache[Constants.BUILD];
            _gameRooms.Update(gameRoom);

            var randomPlayer = players.OrderBy(_ => new Random().NextDouble()).First();
            randomPlayer.IsCurrentPlayer = true;
            _playerSessions.Update(randomPlayer);

            foreach (var player in players)
            {
                foreach (var bonusTypeId in _bonusTypeNameCache.Keys)
                {
                    _playerBonuses.Add(new PlayerBonus() { PlayerId = player.Id, BonusTypeId = bonusTypeId, Number = 0 });
                }
                foreach (var elementId in _elementNameCache.Keys)
                {
                    _playerProduction.Add(new PlayerProduction() { PlayerId = player.Id, ElementId = elementId, Number = 0 });
                }
                foreach (var elementId in _elementNameCache.Keys)
                {
                    _playerResources.Add(new PlayerResource() { PlayerId = player.Id, ElementId = elementId, Number = BASE_ELEMENTS.Contains(_elementNameCache[elementId]) ? 20 : 0 });
                }
            }
        }

        public string PlaceBuilding(string connectionId, string buildingIdentifier, int boardSpace, out string errorMessage)
        {
            errorMessage = null;

            CreateCache();

            // Ensure player exists
            var player = _playerSessions.ReadOne(
                player => player.Token.Equals(connectionId),
                player => player.GameRoom,
                player => player.PlayerBuildings,
                player => player.PlayerResources,
                player => player.PlayerProductions,
                player => player.PlayerBonuses);

            if (player is null)
            {
                errorMessage = "You are not logged in";
                return null;
            }

            // Ensure player is in active game session
            if (player.GameRoomId == null)
            {
                errorMessage = "Your are not currently in a game";
                return null;
            }

            // Ensure game is in build state
            if (!player.GameRoom.StatusTypeId.Equals(_statusTypeIdCache[Constants.BUILD]))
            {
                errorMessage = "The game is not in the build phase";
                return null;
            }

            // Ensure it it currently player's turn
            if (!player.IsCurrentPlayer)
            {
                errorMessage = "It is not currently your turn";
                return null;
            }

            // Ensure no other buildings in spot
            if (player.PlayerBuildings.Any(building => building.BoardSpace.Equals(boardSpace)))
            {
                errorMessage = "There is already a building in this space";
                return null;
            }

            // Ensure building exists
            var building = _buildings.ReadOne(building => building.Identifier.Equals(buildingIdentifier),
            building => building.BuildingCosts);
            if (building is null)
            {
                errorMessage = "Invalid building identifier";
                return null;
            }

            // Ensure player has resources to place building
            if (!building.BuildingCosts.All(cost => cost.Number <= player.PlayerResources.Where(resource => resource.ElementId.Equals(cost.ElementId)).Single().Number))
            {
                errorMessage = "Insufficient resources to place building";
                return null;
            }

            // Create playerbuilding
            var bonusBuildingHealth = player.PlayerBonuses.Where(bonus => bonus.BonusTypeId.Equals(_bonusTypeIdCache[Constants.BUILDING_HEALTH])).Single().Number;
            var playerBuilding = new PlayerBuilding() { BuildingId = building.Id, PlayerId = player.Id, BoardSpace = boardSpace, Health = building.Health + bonusBuildingHealth };
            _playerBuildings.Add(playerBuilding);
            
            // Update playerresources
            foreach (var cost in building.BuildingCosts)
            {
                var playerResource = player.PlayerResources.Where(resource => resource.ElementId.Equals(cost.ElementId)).Single();
                playerResource.Number = playerResource.Number - cost.Number;
                _playerResources.Update(playerResource);
            }
            
            // Update playerproduction
            var playerProduction = player.PlayerProductions.Where(production => building.ElementId.Equals(production.ElementId)).Single();
            playerProduction.Number = playerProduction.Number + building.Production;
            _playerProduction.Update(playerProduction);
            
            // Update playerbonuses
            if (building.Tier.Equals(3))
            {
                if (building.ElementId.Equals(_elementIdCache[Constants.FIRE]))
                {
                    var attackBonus = player.PlayerBonuses.Where(bonus => bonus.BonusTypeId.Equals(_bonusTypeIdCache[Constants.ATTACK])).Single();
                    attackBonus.Number = (player.PlayerBuildings.Where(playerBuilding => playerBuilding.BuildingId.Equals(building.Id)).Count() + 1) > 1 ? 1 : 0;
                    _playerBonuses.Update(attackBonus);
                }
                else if (building.ElementId.Equals(_elementIdCache[Constants.WATER]))
                {
                    var troopHealthBonus = player.PlayerBonuses.Where(bonus => bonus.BonusTypeId.Equals(_bonusTypeIdCache[Constants.TROOP_HEALTH])).Single();
                    troopHealthBonus.Number = (player.PlayerBuildings.Where(playerBuilding => playerBuilding.BuildingId.Equals(building.Id)).Count() + 1) > 1 ? 1 : 0;
                    _playerBonuses.Update(troopHealthBonus);
                }
                else if (building.ElementId.Equals(_elementIdCache[Constants.EARTH]))
                {
                    var buildingHealthBonus = player.PlayerBonuses.Where(bonus => bonus.BonusTypeId.Equals(_bonusTypeIdCache[Constants.BUILDING_HEALTH])).Single();
                    buildingHealthBonus.Number = (player.PlayerBuildings.Where(playerBuilding => playerBuilding.BuildingId.Equals(building.Id)).Count()  + 1 )/ 4;
                    _playerBonuses.Update(buildingHealthBonus);
                }
                else if (building.ElementId.Equals(_elementIdCache[Constants.AIR]))
                {
                    var foodBonus = player.PlayerBonuses.Where(bonus => bonus.BonusTypeId.Equals(_bonusTypeIdCache[Constants.FOOD])).Single();
                    foodBonus.Number = (player.PlayerBuildings.Where(playerBuilding => playerBuilding.BuildingId.Equals(building.Id)).Count() + 1)/ 3;
                    _playerBonuses.Update(foodBonus);
                }
            }
            // Switch to other player's turn
            var opponent = _playerSessions.Read(playerSession => playerSession.GameRoomId.Equals(player.GameRoomId)).Where(gameRoomPlayer => !gameRoomPlayer.Id.Equals(player.Id)).FirstOrDefault();
            opponent.IsCurrentPlayer = true;
            _playerSessions.Update(opponent);
            player.IsCurrentPlayer = false;
            _playerSessions.Update(player);

            return player.GameRoom.Identifier;
        }
    }
}
