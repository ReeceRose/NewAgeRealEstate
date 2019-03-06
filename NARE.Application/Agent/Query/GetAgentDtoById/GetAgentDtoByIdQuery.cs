using MediatR;
using NARE.Application.Agent.Model;

namespace NARE.Application.Agent.Query.GetAgentDtoById
{
    public class GetAgentDtoByIdQuery : IRequest<AgentDto>
    {
        public GetAgentDtoByIdQuery(string agentId) => AgentId = agentId;
        public string AgentId { get; set; }
    }
}
