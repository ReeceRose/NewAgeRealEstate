using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Persistence;

namespace NARE.Application.Listing.Command.CreateListing
{
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public CreateListingCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            await _context.Listings.AddAsync(request.Listing, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
