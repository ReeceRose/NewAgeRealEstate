using AutoMapper;
using NARE.Application.User.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<ApplicationUserDto, ApplicationUser>();
        }
    }
}
