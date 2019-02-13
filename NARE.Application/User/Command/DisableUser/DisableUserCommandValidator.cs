using FluentValidation;

namespace NARE.Application.User.Command.DisableUser
{
    public class DisableUserCommandValidator : AbstractValidator<DisableUserCommand>
    {
        public DisableUserCommandValidator()
        {
            RuleFor(c => c.UserId)
               .NotEmpty().WithMessage("User ID required")
               .NotNull().WithMessage("User ID required");
        }
    }
}
