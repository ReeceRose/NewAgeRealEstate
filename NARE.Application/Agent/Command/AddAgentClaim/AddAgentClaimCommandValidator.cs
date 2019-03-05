using FluentValidation;

namespace NARE.Application.Agent.Command.AddAgentClaim
{
    public class AddAgentClaimCommandValidator : AbstractValidator<AddAgentClaimCommand>
    {
        public AddAgentClaimCommandValidator()
        {
            RuleFor(c => c.Agent)
                .NotNull().WithMessage("Agent required");

            RuleFor(c => c.Key)
                .NotEmpty().WithMessage("Key required")
                .NotNull().WithMessage("Key required");

            RuleFor(c => c.Value)
                .NotEmpty().WithMessage("Value required")
                .NotEmpty().WithMessage("Value required");
        }
    }
}
