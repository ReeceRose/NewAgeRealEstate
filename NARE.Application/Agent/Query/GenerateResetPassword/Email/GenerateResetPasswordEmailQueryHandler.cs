using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NARE.Application.Agent.Query.GenerateResetPassword.Token;
using NARE.Application.Agent.Query.GetAgentByEmail;
using NARE.Application.Interfaces;

namespace NARE.Application.Agent.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailQueryHandler : IRequestHandler<GenerateResetPasswordEmailQuery, string>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GenerateResetPasswordEmailQueryHandler> _logger;

        public GenerateResetPasswordEmailQueryHandler(IMediator mediator, INotificationService notificationService, IConfiguration configuration, ILogger<GenerateResetPasswordEmailQueryHandler> logger)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Handle(GenerateResetPasswordEmailQuery request, CancellationToken cancellationToken)
        {
            var agent = _mediator.Send(new GetAgentByEmailQuery(request.Email), cancellationToken).Result;

            if (agent == null)
            {
                return await Task.FromResult<string>(null);
            }
            _logger.LogInformation($"Generate Reset Password Email: {request.Email}: Requesting email");

            var token = _mediator.Send(new GenerateResetPasswordTokenQuery(agent), cancellationToken).Result;

            await _notificationService.SendNotificationAsync(toName: request.Email, toEmailAddress: request.Email, subject: "Password reset",
                message: $"You have requested a password reset. To reset our password click <a href='{_configuration["FrontEndUrl"]}/Agent/ResetPassword?email={agent.Email}&token={Base64UrlEncoder.Encode(token)}'>here</a>");
            _logger.LogInformation($"Generate Reset Password Email: {request.Email}: Sent email");
            return await Task.FromResult(token);
        }
    }
}