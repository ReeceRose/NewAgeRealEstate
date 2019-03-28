using FluentValidation;

namespace NARE.Application.Agent.Command.UpdateAgent
{
    public class UpdateAgentCommandValidator : AbstractValidator<UpdateAgentCommand>
    {
        public UpdateAgentCommandValidator()
        {
            RuleFor(c => c.Agent)
                .NotNull().WithMessage("Agent required");
        }
    }
}
