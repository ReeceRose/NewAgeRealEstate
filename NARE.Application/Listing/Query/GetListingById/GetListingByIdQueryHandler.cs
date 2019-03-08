using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NARE.Persistence;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, Domain.Entities.Listing>
    {
        private readonly ApplicationDbContext _context;

        public GetListingByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Listing> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Listings.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken: cancellationToken);
        }
    }
}
