using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQueryHandler : IRequestHandler<GenerateResetPasswordTokenQuery, string>
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public GenerateResetPasswordTokenQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(GenerateResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(await _userManager.GeneratePasswordResetTokenAsync(request.User));
        }
    }
}