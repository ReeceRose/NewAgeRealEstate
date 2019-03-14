using MediatR;

namespace NARE.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationCommand : IRequest<bool>
    {
        public UpdateAgentInformationCommand(Domain.Entities.Agent agent) => Agent = agent;
        public Domain.Entities.Agent Agent { get; set; }
    }
}
