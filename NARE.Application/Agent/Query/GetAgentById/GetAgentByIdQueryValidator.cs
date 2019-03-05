using FluentValidation;

namespace NARE.Application.Agent.Query.GetAgentById
{
    public class GetAgentByIdQueryValidator : AbstractValidator<GetAgentByIdQuery>
    {
        public GetAgentByIdQueryValidator()
        {
            RuleFor(c => c.AgentID)
                .NotEmpty().WithMessage("Agent ID required")
                .NotNull().WithMessage("Agent ID required");
        }
    }
}
