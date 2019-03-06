using MediatR;

namespace NARE.Application.Agent.Query.LoginAgent
{
    public class LoginAgentQuery : IRequest<string>
    {
        public LoginAgentQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}