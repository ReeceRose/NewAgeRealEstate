using MediatR;
using Microsoft.AspNetCore.Identity;
using NARE.Application.User.Model;

namespace NARE.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(ApplicationUserDto user, string password)
        {
            User = user;
            Password = password;
        }

        public ApplicationUserDto User { get; }
        public string Password { get; set; }
    }
}
