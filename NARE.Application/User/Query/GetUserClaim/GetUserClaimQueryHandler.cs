using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NARE.Domain.Entities;

namespace NARE.Application.User.Query.GetUserClaim
{
    public class GetUserClaimQueryHandler : IRequestHandler<GetUserClaimQuery, List<Claim>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserClaimQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<Claim>> Handle(GetUserClaimQuery request, CancellationToken cancellationToken)
        {
            var claims = await _userManager.GetClaimsAsync(_mapper.Map<ApplicationUser>(request.User));
            return claims.ToList();
        }
    }
}
