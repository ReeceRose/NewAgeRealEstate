using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GenerateResetPassword.Token
{
    public class GenerateResetPasswordTokenQuery : IRequest<string>
    {
        public GenerateResetPasswordTokenQuery(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}
