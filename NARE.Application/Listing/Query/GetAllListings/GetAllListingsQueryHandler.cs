using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Persistence;

namespace NARE.Application.Listing.Query.GetAllListings
{
    public class GetAllListingsQueryHandler : IRequestHandler<GetAllListingsQuery, List<Domain.Entities.Listing>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllListingsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Listing>> Handle(GetAllListingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Listings.ToListAsync(cancellationToken);
        }
    }
}
