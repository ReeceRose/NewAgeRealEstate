using FluentValidation;

namespace NARE.Application.Agent.Query.GetAgentClaim
{
    public class GetAgentClaimQueryValidator : AbstractValidator<GetAgentClaimQuery>
    {
        public GetAgentClaimQueryValidator()
        {
            RuleFor(q => q.Agent)
                .NotEmpty().WithMessage("Agent required")
                .NotNull().WithMessage("Agent required");
        }
    }
}
