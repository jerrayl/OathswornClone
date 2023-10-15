using Oathsworn.Entities;
using AutoMapper;
using Oathsworn.Models;

namespace Oathsworn.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<MightCard, MightCardModel>();
            CreateMap<EncounterPlayer, PlayerModel>()
                .ForMember(dest => dest.Class, m => m.MapFrom(src => src.Player.Class))
                .ForMember(dest => dest.Defence, m => m.MapFrom(src => src.Player.Defence))
                .ForMember(dest => dest.MaxAnimus, m => m.MapFrom(src => src.Player.MaxAnimus))
                .ForMember(dest => dest.AnimusRegen, m => m.MapFrom(src => src.Player.AnimusRegen))
                .ForMember(dest => dest.Might, m => m.MapFrom(src => src.Player.Might));
            CreateMap<Boss, BossModel>();
        }
    }
}

