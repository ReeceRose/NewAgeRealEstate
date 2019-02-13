using MediatR;

namespace NARE.Application.User.Command.DisableUser
{
    public class DisableUserCommand : IRequest<bool>
    {
        public DisableUserCommand(string userId) => UserId = userId;

        public string UserId { get; }
    }
}
