using MediatR;

namespace NARE.Application.Agent.Command.DisableAgent
{
    public class DisableAgentCommand : IRequest<bool>
    {
        public DisableAgentCommand(string agentId) => AgentId = agentId;

        public string AgentId { get; }
    }
}
