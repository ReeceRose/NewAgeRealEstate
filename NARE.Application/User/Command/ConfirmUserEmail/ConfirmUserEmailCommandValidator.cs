using FluentValidation;

namespace NARE.Application.User.Command.ConfirmUserEmail
{
    public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
    {
        public ConfirmUserEmailCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("User ID required");
            RuleFor(r => r.Token)
                .NotEmpty().WithMessage("Token required");
        }
    }
}
