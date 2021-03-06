﻿using FluentValidation;

namespace NARE.Application.Agent.Command.NewAgent
{
    public class NewAgentCommandValidator : AbstractValidator<NewAgentCommand>
    {
        public NewAgentCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required");

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
