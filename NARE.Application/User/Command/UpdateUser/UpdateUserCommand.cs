using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UpdateUserCommand(ApplicationUser user) => User = user;

        public ApplicationUser User { get; }
    }
}
