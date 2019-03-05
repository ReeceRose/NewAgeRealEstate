using FluentValidation;

namespace NARE.Application.Agent.Command.DisableAgent
{
    public class DisableAgentCommandValidator : AbstractValidator<DisableAgentCommand>
    {
        public DisableAgentCommandValidator()
        {
            RuleFor(c => c.AgentId)
               .NotEmpty().WithMessage("Agent ID required")
               .NotNull().WithMessage("Agent ID required");
        }
    }
}
