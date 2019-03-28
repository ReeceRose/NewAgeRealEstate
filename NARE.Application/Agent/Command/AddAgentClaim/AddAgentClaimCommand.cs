using MediatR;

namespace NARE.Application.Agent.Command.AddAgentClaim
{
    public class AddAgentClaimCommand : IRequest<bool>
    {
        public AddAgentClaimCommand(Domain.Entities.Agent agent, string key, string value)
        {
            Agent = agent;
            Key = key;
            Value = value;
        }

        public Domain.Entities.Agent Agent { get; set; }
        public string Key { get; }
        public string Value { get; }
    }
}
