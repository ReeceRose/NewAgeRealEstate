using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Application.Agent.Query.GetAgentById;

namespace NARE.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationCommandHandler : IRequestHandler<UpdateAgentInformationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UpdateAgentInformationCommandHandler> _logger;

        public UpdateAgentInformationCommandHandler(IMediator mediator, ILogger<UpdateAgentInformationCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateAgentInformationCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(new GetAgentByIdQuery(request.Agent.Id), cancellationToken);
            // Add any other updates here
            agent.Name = request.Agent.Name;
            agent.ImageUrl = request.Agent.ImageUrl;
            agent.PhoneNumber = request.Agent.PhoneNumber;
            _logger.LogInformation($"Update Agent Information: {agent.Email}: Successful update");
            return await Task.FromResult(await _mediator.Send(new UpdateAgentCommand(agent), cancellationToken));
        }
    }
}
