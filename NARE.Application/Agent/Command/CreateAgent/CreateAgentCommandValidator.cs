using FluentValidation;

namespace NARE.Application.Agent.Command.CreateAgent
{
    public class CreateAgentCommandValidator : AbstractValidator<CreateAgentCommand>
    {
        public CreateAgentCommandValidator()
        {
            RuleFor(c => c.Agent)
                .NotNull().WithMessage("Agent required");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password required")
                .NotNull().WithMessage("Password required");
        }
    }
}
