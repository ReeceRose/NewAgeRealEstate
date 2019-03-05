using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace NARE.Application.Agent.Command.RemoveAgentClaim
{
    public class RemoveAgentClaimCommandHandler : IRequestHandler<RemoveAgentClaimCommand, bool>
    {
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly ILogger<RemoveAgentClaimCommandHandler> _logger;

        public RemoveAgentClaimCommandHandler(UserManager<Domain.Entities.Agent> userManager, ILogger<RemoveAgentClaimCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        
        public async Task<bool> Handle(RemoveAgentClaimCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Remove Claim: {request.Agent.Email}: Removing claim ({request.Key})");
            var result = await _userManager.RemoveClaimAsync(request.Agent, new Claim(request.Key, ""));
            return result.Succeeded;
        }
    }
}
