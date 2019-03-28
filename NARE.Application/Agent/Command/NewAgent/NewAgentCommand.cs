using MediatR;

namespace NARE.Application.Agent.Command.NewAgent
{
    public class NewAgentCommand : IRequest<bool>
    {
        public NewAgentCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; }
        public string Password { get; }
    }
}
