using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Repositories;
using Oathsworn.Models;
using AutoMapper;
using Oathsworn.Business.Constants;
using Oathsworn.Infrastructure;
using Oathsworn.Extensions;

namespace Oathsworn.Business
{
    public interface IUserManagement
    {
        List<EncounterModel> GetEncounters();
        List<FreeCompanyModel> GetFreeCompanies();
        List<PlayerSummaryModel> GetPlayers();

        void CreatePlayer(PlayerSummaryModel model);
        void CreateFreeCompany(CreateFreeCompanyModel model);
        void JoinFreeCompany(JoinFreeCompanyModel model);
    }

    public class UserManagement(
        UserContext userContext,
        IDatabaseRepository<FreeCompany> freeCompanies,
        IDatabaseRepository<Player> players,
        IMapper mapper
        ) : IUserManagement
    {
        private readonly UserContext _userContext = userContext;
        private readonly IDatabaseRepository<FreeCompany> _freeCompanies = freeCompanies;
        private readonly IDatabaseRepository<Player> _players = players;
        private readonly IMapper _mapper = mapper;

        public void CreatePlayer(PlayerSummaryModel model)
        {
            var player = _mapper.Map<Player>(DefaultPlayers.Players[model.Class]); //Use mapper to clone player
            player.Name = model.Name;
            player.UserId = _userContext.Id;
            _players.Add(player);
        }

        public void CreateFreeCompany(CreateFreeCompanyModel model)
        {
            var player = _players.ReadOne(x => x.UserId == _userContext.Id && x.Id == model.PlayerId)
                ?? throw new ErrorMessageException("Player is invalid");

            var freeCompany = new FreeCompany()
            {
                Name = model.Name,
                Code = StringExtensions.RandomString(4) //Assume this is unique for now. Will need to check for duplicates in the future
            };

            _freeCompanies.Add(freeCompany);
            player.FreeCompanyId = freeCompany.Id;
            _players.Update(player);
        }

        public void JoinFreeCompany(JoinFreeCompanyModel model)
        {
            var player = _players.ReadOne(x => x.UserId == _userContext.Id && x.Id == model.PlayerId)
                ?? throw new ErrorMessageException("Player is invalid");

            var freeCompany = _freeCompanies.ReadOne(x => x.Code.Equals(model.Code));

            player.FreeCompanyId = freeCompany.Id;
            _players.Update(player);
        }

        public List<EncounterModel> GetEncounters()
        {
            return _players.Read(x => x.UserId == _userContext.Id, x => x.FreeCompany, x => x.EncounterPlayer.Encounter, x => x.EncounterPlayer.Encounter.Boss)
                .DistinctBy(x => x.EncounterPlayer.EncounterId)
                .Where(x => x.EncounterPlayer is not null)
                .Select(x => new EncounterModel
                {
                    EncounterId = x.EncounterPlayer.Encounter.Id,
                    EncounterNumber = x.EncounterPlayer.Encounter.Boss.Number,
                    FreeCompanyName = x.FreeCompany.Name,
                    DateStarted = x.EncounterPlayer.Encounter.DateStarted
                })
                .ToList();
        }

        public List<FreeCompanyModel> GetFreeCompanies()
        {
            var freeCompanyIds = _players.Read(x => x.UserId == _userContext.Id)
                .Select(x => x.FreeCompanyId)
                .Where(x => x is not null)
                .Distinct();

            var freeCompanies = _freeCompanies.Read(x => freeCompanyIds.Contains(x.Id), x => x.Players);

            return freeCompanies.Select(_mapper.Map<FreeCompanyModel>).ToList();
        }

        public List<PlayerSummaryModel> GetPlayers()
        {
            var players = _players.Read(x => x.UserId == _userContext.Id && x.FreeCompanyId is null);

            return players.Select(_mapper.Map<PlayerSummaryModel>).ToList();
        }
    }
}
