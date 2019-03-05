using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Command.UpdateAgent;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions;

namespace NARE.Application.Agent.Command.EnableAgent
{
    public class EnableAgentCommandHandler: IRequestHandler<EnableAgentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EnableAgentCommandHandler> _logger;

        public EnableAgentCommandHandler(IMediator mediator, ILogger<EnableAgentCommandHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<bool> Handle(EnableAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(new GetAgentByIdQuery(request.AgentId), cancellationToken);
            // Should not be the case as the ID is not being touched by the agent at all
            if (agent == null)
            {
                throw new InvalidAgentException();
            }

            agent.AccountEnabled = true;
            _logger.LogInformation($"Enable Agent: {request.AgentId}: Account enabled");
            await _mediator.Send(new UpdateAgentCommand(agent), cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
