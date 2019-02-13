using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQuery : IRequest<string>
    {
        public GenerateEmailConfirmationTokenQuery(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}