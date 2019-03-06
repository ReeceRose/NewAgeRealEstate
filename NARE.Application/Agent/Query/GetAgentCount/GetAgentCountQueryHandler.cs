using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Agent.Query.GetAllAgents;

namespace NARE.Application.Agent.Query.GetAgentCount
{
    public class GetAgentCountQueryHandler : IRequestHandler<GetAgentCountQuery, int>
    {
        private readonly IMediator _mediator;

        public GetAgentCountQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(GetAgentCountQuery request, CancellationToken cancellationToken)
        {
            var agents = await _mediator.Send(new GetAllAgentsQuery(), cancellationToken);
            return agents?.Count ?? 0;
        }
    }
}
