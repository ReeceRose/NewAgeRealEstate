using MediatR;
using NARE.Domain.Entities;

namespace NARE.Application.User.Command.AddUserClaim
{
    public class AddUserClaimCommand : IRequest<bool>
    {
        public AddUserClaimCommand(ApplicationUser user, string key, string value)
        {
            User = user;
            Key = key;
            Value = value;
        }

        public ApplicationUser User { get; set; }
        public string Key { get; }
        public string Value { get; }
    }
}
