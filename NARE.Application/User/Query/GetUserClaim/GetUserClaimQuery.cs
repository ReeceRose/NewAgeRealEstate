using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using NARE.Application.User.Model;

namespace NARE.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQuery : IRequest<List<Claim>>
    {
        public GetUserClaimQuery(ApplicationUserDto user) => User = user;

        public ApplicationUserDto User { get; }
    }
}
