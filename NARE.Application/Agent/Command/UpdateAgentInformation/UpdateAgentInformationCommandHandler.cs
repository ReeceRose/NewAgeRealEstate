using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Application.Agent.Query.GetAgentById;

namespace NARE.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationCommandHandler : IRequestHandler<UpdateAgentInformationCommand, bool>
    {
        private readonly IMediator _mediator;

        public UpdateAgentInformationCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateAgentInformationCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(new GetAgentByIdQuery(request.Agent.Id), cancellationToken);
            // Add any other updates here
            agent.Name = request.Agent.Name;
            return await Task.FromResult(await _mediator.Send(new UpdateAgentCommand(agent), cancellationToken));
        }
    }
}
