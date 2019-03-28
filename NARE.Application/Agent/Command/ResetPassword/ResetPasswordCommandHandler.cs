using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Domain.Exceptions.Account;
using NARE.Domain.Exceptions.Agent;

namespace NARE.Application.Agent.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IMediator mediator, UserManager<Domain.Entities.Agent> userManager, ILogger<ResetPasswordCommandHandler> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var agent = _mediator.Send(new GetAgentByEmailQuery(request.Email), cancellationToken).Result;

            if (agent == null)
            {
                _logger.LogInformation($"Reset Password: {request.Email}: Failed reset: Agent does not exist");
                throw new InvalidAgentException();
            }

            var token = Base64UrlEncoder.Decode(request.Token);

            var result = await _userManager.ResetPasswordAsync(agent, token, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Reset Password: {request.Email}: Failed reset: {result.Errors}");
                throw new FailedToResetPassword();
            }

            _logger.LogInformation($"Reset Password: {request.Email}: Successful reset");
            return await Task.FromResult(true);
        }
    }
}