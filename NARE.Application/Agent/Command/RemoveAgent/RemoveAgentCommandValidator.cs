using FluentValidation;

namespace NARE.Application.Agent.Command.RemoveAgent
{
    public class RemoveAgentCommandValidator : AbstractValidator<RemoveAgentCommand>
    {
        public RemoveAgentCommandValidator()
        {
            RuleFor(c => c.AgentId)
                .NotEmpty().WithMessage("Agent ID required")
                .NotNull().WithMessage("Agent ID required");
        }
    }
}
