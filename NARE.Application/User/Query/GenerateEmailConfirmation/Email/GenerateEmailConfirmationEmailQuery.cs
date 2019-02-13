﻿using MediatR;

namespace NARE.Application.User.Query.GenerateEmailConfirmation.Email
{
    public class GenerateEmailConfirmationEmailQuery : IRequest<string>
    {
        public GenerateEmailConfirmationEmailQuery(string email) => Email = email;

        public string Email { get; }
    }
}
