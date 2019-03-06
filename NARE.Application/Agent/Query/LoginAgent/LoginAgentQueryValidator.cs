using FluentValidation;

namespace NARE.Application.Agent.Query.LoginAgent
{
    public class LoginAgentQueryValidator : AbstractValidator<LoginAgentQuery>
    {
        public LoginAgentQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is required");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password does not meet security constraints")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$")
                .WithMessage("Password does not meet security constraints");
        }
    }
}
