﻿using FluentValidation;

namespace NARE.Application.User.Query.GenerateEmailConfirmation.Token
{
    public class GenerateEmailConfirmationTokenQueryValidator : AbstractValidator<GenerateEmailConfirmationTokenQuery>
    {
        public GenerateEmailConfirmationTokenQueryValidator()
        {
            RuleFor(u => u.User)
                .NotNull().WithMessage("User required");
        }
    }
}
