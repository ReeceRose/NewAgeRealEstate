using FluentValidation;

namespace NARE.Application.Agent.Query.SearchAgentsByEmail
{
    public class SearchAgentsByEmailQueryValidator : AbstractValidator<SearchAgentsByEmailQuery>
    {
        public SearchAgentsByEmailQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
