using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NARE.Application.Agent.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQueryHandler : IRequestHandler<GenerateResetPasswordTokenQuery, string>
    {

        private readonly UserManager<Domain.Entities.Agent> _userManager;

        public GenerateResetPasswordTokenQueryHandler(UserManager<Domain.Entities.Agent> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(GenerateResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userManager.GeneratePasswordResetTokenAsync(request.Agent));
        }
    }
}