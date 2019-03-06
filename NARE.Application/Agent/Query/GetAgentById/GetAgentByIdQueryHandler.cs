using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Persistence;

namespace NARE.Application.Agent.Query.GetAgentById
{
    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, Domain.Entities.Agent>
    {
        private readonly ApplicationDbContext _context;

        public GetAgentByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Agent> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            return(await _context.Users.FirstOrDefaultAsync(u => u.Id == request.AgentID, cancellationToken: cancellationToken));
        }
    }
}
