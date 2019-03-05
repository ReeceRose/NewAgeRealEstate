using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Persistence;

namespace NARE.Application.Agent.Query.GetAgentByEmail
{
    public class GetAgentByEmailQueryHandler : IRequestHandler<GetAgentByEmailQuery, Domain.Entities.Agent>
    {
        private readonly ApplicationDbContext _context;

        public GetAgentByEmailQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Agent> Handle(GetAgentByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(
                u => string.Equals(u.Email, request.Email, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken: cancellationToken);
        }
    }
}