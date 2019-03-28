using System.Collections.Generic;
using MediatR;
using NARE.Application.Agent.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Query.GetPaginatedAgentsResult
{
    public class GetPaginatedAgentResultQuery : IRequest<PaginatedAgentsDto>
    {
        public GetPaginatedAgentResultQuery(List<AgentDto> agents, PaginationModel paginationModel)
        {
            Agents = agents;
            PaginationModel = paginationModel;
        }

        public List<AgentDto> Agents { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
