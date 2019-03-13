using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Agent.Model;

namespace NARE.Application.Agent.Query.GetPaginatedAgentsResult
{
    public class GetPaginatedAgentsResultQueryHandler : IRequestHandler<GetPaginatedAgentResultQuery, PaginatedAgentsDto>
    {
        public Task<PaginatedAgentsDto> Handle(GetPaginatedAgentResultQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PaginatedAgentsDto()
            {
                PaginationModel = request.PaginationModel,
                Agents = request.Agents
            });
        }
    }
}
