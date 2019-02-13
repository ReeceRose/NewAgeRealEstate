﻿using MediatR;

namespace NARE.Application.User.Query.LoginUser
{
    public class LoginUserQuery : IRequest<string>
    {
        public LoginUserQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}