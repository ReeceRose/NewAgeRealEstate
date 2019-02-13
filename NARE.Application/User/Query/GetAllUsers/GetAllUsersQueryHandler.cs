using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Domain.Entities;
using NARE.Persistence;

namespace NARE.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
