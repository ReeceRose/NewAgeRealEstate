using MediatR;

namespace NARE.Application.Agent.Command.RemoveAgentClaim
{
    public class RemoveAgentClaimCommand : IRequest<bool>
    {
        public RemoveAgentClaimCommand(Domain.Entities.Agent agent, string key)
        {
            Agent = agent;
            Key = key;
        }

        public Domain.Entities.Agent Agent { get; set; }
        public string Key { get; }
    }
}
