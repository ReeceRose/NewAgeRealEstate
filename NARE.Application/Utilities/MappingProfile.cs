using AutoMapper;
using NARE.Application.Agent.Model;

namespace NARE.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Agent, AgentDto>();

            CreateMap<AgentDto, Domain.Entities.Agent>();
        }
    }
}
