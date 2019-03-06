using MediatR;

namespace NARE.Application.Agent.Command.NewAgent
{
    public class NewAgentCommand : IRequest<bool>
    {
        public NewAgentCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
