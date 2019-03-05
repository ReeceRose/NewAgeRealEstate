using FluentValidation;

namespace NARE.Application.Agent.Command.EnableAgent
{
    public class EnableAgentCommandValidator : AbstractValidator<EnableAgentCommand>
    {
        public EnableAgentCommandValidator()
        {
            RuleFor(c => c.AgentId)
                .NotEmpty().WithMessage("Agent ID required")
                .NotNull().WithMessage("Agent ID required");
        }
    }
}
