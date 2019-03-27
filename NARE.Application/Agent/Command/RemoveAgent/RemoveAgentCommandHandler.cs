using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NARE.Application.Agent.Query.GetAgentById;
using NARE.Domain.Exceptions.Agent;

namespace NARE.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentCommandHandler : IRequestHandler<RemoveAgentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly ILogger<RemoveAgentCommandHandler> _logger;

        public RemoveAgentCommandHandler(IMediator mediator, UserManager<Domain.Entities.Agent> userManager, ILogger<RemoveAgentCommandHandler> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(RemoveAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await _mediator.Send(new GetAgentByIdQuery(request.AgentId), cancellationToken);
            if (agent == null)
            {
                _logger.LogInformation($"Remove Agent: {request.AgentId}: Removal failed: Agent does not exist");
                throw new InvalidAgentException();
            }

            var result = await _userManager.DeleteAsync(agent);

            _logger.LogInformation($"Remove Agent: {request.AgentId}: Removal success");

            return await Task.FromResult(result.Succeeded);
        }
    }
}
