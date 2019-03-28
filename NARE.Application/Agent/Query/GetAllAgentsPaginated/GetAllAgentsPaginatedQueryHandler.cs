using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Agent.Model;
using NARE.Application.Agent.Query.GetAllAgents;
using NARE.Application.Agent.Query.GetPaginatedAgentsResult;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Query.GetAllAgentsPaginated
{
    public class GetAllAgentsPaginatedQueryHandler : IRequestHandler<GetAllAgentsPaginatedQuery, PaginatedAgentsDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllAgentsPaginatedQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedAgentsDto> Handle(GetAllAgentsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var allAgents = await _mediator.Send(new GetAllAgentsQuery(), cancellationToken);
            var orderedAgents = allAgents.OrderBy(u => u.Email);
            request.PaginationModel.Count = orderedAgents.Count();
            var paginatedAgents = orderedAgents.Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();
            var result = _mapper.Map<List<AgentDto>>(paginatedAgents.ToList());

            return await _mediator.Send(new GetPaginatedAgentResultQuery(result, request.PaginationModel), cancellationToken);
        }
    }
}
