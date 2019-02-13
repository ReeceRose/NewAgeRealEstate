using MediatR;

namespace NARE.Application.User.Query.GenerateResetPassword.Email
{
    public class GenerateResetPasswordEmailQuery : IRequest<string>
    {
        public GenerateResetPasswordEmailQuery(string email) => Email = email;

        public string Email { get; }
    }
}