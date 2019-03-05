using MediatR;

namespace NARE.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentCommand : IRequest<bool>
    {
        public RemoveAgentCommand(string agentId) => AgentId = agentId;

        public string AgentId { get; }
    }
}
