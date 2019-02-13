using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Domain.Entities;
using NARE.Persistence;

namespace NARE.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return(await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken));
        }
    }
}
