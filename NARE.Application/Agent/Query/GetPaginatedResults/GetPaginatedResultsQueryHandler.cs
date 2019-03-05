using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Agent.Model;

namespace NARE.Application.Agent.Query.GetPaginatedResults
{
    public class GetPaginatedResultsQueryHandler : IRequestHandler<GetPaginatedResultsQuery, PaginatedAgentsDto>
    {
        public Task<PaginatedAgentsDto> Handle(GetPaginatedResultsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PaginatedAgentsDto()
            {
                PaginationModel = request.PaginationModel,
                Agents = request.Agents
            });
        }
    }
}
