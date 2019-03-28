using AutoMapper;
using NARE.Application.Agent.Command.NewAgent;
using NARE.Domain.Entities;

namespace NARE.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Agent, AgentDto>();

            CreateMap<AgentDto, Domain.Entities.Agent>();

            CreateMap<NewAgentCommand, AgentDto>();
        }
    }
}
