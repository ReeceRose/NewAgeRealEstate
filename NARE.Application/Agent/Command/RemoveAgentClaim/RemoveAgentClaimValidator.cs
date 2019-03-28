using FluentValidation;

namespace NARE.Application.Agent.Command.RemoveAgentClaim
{
    public class RemoveAgentClaimValidator : AbstractValidator<RemoveAgentClaimCommand>
    {
        public RemoveAgentClaimValidator()
        {
            RuleFor(c => c.Agent)
                .NotNull().WithMessage("Agent required");

            RuleFor(c => c.Key)
                .NotEmpty().WithMessage("Key required")
                .NotNull().WithMessage("Key required");
        }
    }
}
