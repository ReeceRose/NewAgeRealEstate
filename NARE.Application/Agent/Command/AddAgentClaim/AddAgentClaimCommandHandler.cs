using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace NARE.Application.Agent.Command.AddAgentClaim
{
    public class AddAgentClaimCommandHandler : IRequestHandler<AddAgentClaimCommand, bool>
    {
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly ILogger<AddAgentClaimCommandHandler> _logger;

        public AddAgentClaimCommandHandler(UserManager<Domain.Entities.Agent> userManager, ILogger<AddAgentClaimCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(AddAgentClaimCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Add Claim: {request.Agent.Email}: Adding claim ({request.Key}, {request.Value})");
            var result = await _userManager.AddClaimAsync(request.Agent, new Claim(request.Key, request.Value));
            return result.Succeeded;
        }
    }
}
