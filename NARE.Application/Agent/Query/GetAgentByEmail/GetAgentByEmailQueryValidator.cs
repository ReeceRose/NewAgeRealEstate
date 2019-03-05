using FluentValidation;

namespace NARE.Application.Agent.Query.GetAgentByEmail
{
    public class GetAgentByEmailQueryValidator : AbstractValidator<GetAgentByEmailQuery>
    {
        public GetAgentByEmailQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");
        }
    }
}
