using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Query.GetAgentDtoById
{
    public class GetAgentDtoByIdQueryHandler : IRequestHandler<GetAgentDtoByIdQuery, AgentDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAgentDtoByIdQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<AgentDto> Handle(GetAgentDtoByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<AgentDto>(await _mediator.Send(new GetAgentByIdQuery(request.AgentId), cancellationToken));
        }
    }
}
