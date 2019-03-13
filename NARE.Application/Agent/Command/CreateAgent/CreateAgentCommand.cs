using MediatR;
using Microsoft.AspNetCore.Identity;
using NARE.Domain.Entities;

namespace NARE.Application.Agent.Command.CreateAgent
{
    public class CreateAgentCommand : IRequest<IdentityResult>
    {
        public CreateAgentCommand(AgentDto agent, string password)
        {
            Agent = agent;
            Password = password;
        }

        public AgentDto Agent { get; }
        public string Password { get; set; }
    }
}
