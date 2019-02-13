using FluentValidation;

namespace NARE.Application.User.Command.EnableUser
{
    public class EnableUserCommandValidator : AbstractValidator<EnableUserCommand>
    {
        public EnableUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID required")
                .NotNull().WithMessage("User ID required");
        }
    }
}
