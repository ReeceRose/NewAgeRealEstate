using MediatR;

namespace NARE.Application.Agent.Query.GetAgentById
{
    public class GetAgentByIdQuery : IRequest<Domain.Entities.Agent>
    {
        public GetAgentByIdQuery(string agentId) => AgentID = agentId;
        public string AgentID { get; set; }
    }
}
