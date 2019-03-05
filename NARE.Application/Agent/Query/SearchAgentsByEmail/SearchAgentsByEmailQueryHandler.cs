using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetAllAgents;
using NARE.Application.Agent.Query.GetPaginatedResults;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Query.SearchAgentsByEmail
{
    public class SearchAgentsByEmailQueryHandler : IRequestHandler<SearchAgentsByEmailQuery, PaginatedAgentsDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchAgentsByEmailQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedAgentsDto> Handle(SearchAgentsByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllAgentsQuery(), cancellationToken);
            var agents = _mapper.Map<List<AgentDto>>(result.Where(u => u.Email.Contains(request.Email)));
            var model = new PaginationModel {Count = agents.Count};
            return await _mediator.Send(new GetPaginatedResultsQuery(agents, model), cancellationToken);
        }
    }
}
