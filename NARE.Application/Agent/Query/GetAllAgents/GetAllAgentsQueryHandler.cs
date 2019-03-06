using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Persistence;

namespace NARE.Application.Agent.Query.GetAllAgents
{
    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, List<Domain.Entities.Agent>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllAgentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Agent>> Handle(GetAllAgentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
