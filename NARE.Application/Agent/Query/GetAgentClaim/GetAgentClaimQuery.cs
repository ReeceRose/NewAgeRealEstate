using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using NARE.Application.Agent.Model;

namespace NARE.Application.Agent.Query.GetAgentClaim
{
    public class GetAgentClaimQuery : IRequest<List<Claim>>
    {
        public GetAgentClaimQuery(AgentDto agent) => Agent = agent;

        public AgentDto Agent { get; }
    }
}
