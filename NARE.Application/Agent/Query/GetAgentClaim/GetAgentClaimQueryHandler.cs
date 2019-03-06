using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NARE.Application.Agent.Query.GetAgentClaim
{
    public class GetAgentClaimQueryHandler : IRequestHandler<GetAgentClaimQuery, List<Claim>>
    {
        private readonly UserManager<Domain.Entities.Agent> _userManager;
        private readonly IMapper _mapper;

        public GetAgentClaimQueryHandler(UserManager<Domain.Entities.Agent> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<Claim>> Handle(GetAgentClaimQuery request, CancellationToken cancellationToken)
        {
            var claims = await _userManager.GetClaimsAsync(_mapper.Map<Domain.Entities.Agent>(request.Agent));
            return claims.ToList();
        }
    }
}
