using RAZE.Models;
using RAZE.Entities;
using AutoMapper;

namespace RAZE.AutoMapper
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
            CreateMap<Building, BuildingModel>()
                .ForMember(dest => dest.Element, m => m.MapFrom(src => src.Element.Name));
        }
    }
}

