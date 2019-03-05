using FluentValidation;

namespace NARE.Application.Agent.Query.GetAgentDtoById
{
    public class GetAgentByIdQueryValidator : AbstractValidator<GetAgentDtoByIdQuery>
    {
        public GetAgentByIdQueryValidator()
        {
            RuleFor(c => c.AgentId)
                .NotEmpty().WithMessage("Agent ID required")
                .NotNull().WithMessage("Agent ID required");
        }
    }
}
