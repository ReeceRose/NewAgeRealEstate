using FluentValidation;

namespace NARE.Application.User.Command.RemoveUser
{
    public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID required")
                .NotNull().WithMessage("User ID required");
        }
    }
}
