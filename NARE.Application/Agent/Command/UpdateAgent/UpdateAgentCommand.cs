using MediatR;

namespace NARE.Application.Agent.Command.UpdateAgent
{
    public class UpdateAgentCommand : IRequest<bool>
    {
        public UpdateAgentCommand(Domain.Entities.Agent agent) => Agent = agent;

        public Domain.Entities.Agent Agent { get; }
    }
}
