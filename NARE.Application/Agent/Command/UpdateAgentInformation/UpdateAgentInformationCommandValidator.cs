using FluentValidation;

namespace NARE.Application.Agent.Command.UpdateAgentInformation
{
    public class UpdateAgentInformationCommandValidator : AbstractValidator<UpdateAgentInformationCommand>
    {
        public UpdateAgentInformationCommandValidator()
        {
            RuleFor(c => c.Agent)
                .NotNull().WithMessage("Agent required");
        }
    }
}
