using MediatR;

namespace NARE.Application.Agent.Command.EnableAgent
{
    public class EnableAgentCommand : IRequest<bool>
    {
        public EnableAgentCommand(string agentId) => AgentId = agentId;

        public string AgentId { get; }
    }
}
