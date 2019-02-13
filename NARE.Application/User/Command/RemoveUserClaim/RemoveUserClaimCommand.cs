using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Command.RemoveUserClaim
{
    public class RemoveUserClaimCommand : IRequest<bool>
    {
        public RemoveUserClaimCommand(ApplicationUser user, string key)
        {
            User = user;
            Key = key;
        }

        public ApplicationUser User { get; set; }
        public string Key { get; }
    }
}
