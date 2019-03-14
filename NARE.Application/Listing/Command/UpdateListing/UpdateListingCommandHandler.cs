using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Domain.Entities;
using NARE.Persistence;
using Enum = System.Enum;

namespace NARE.Application.Listing.Command.UpdateListing
{
    public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateListingCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            request.Listing.Status = (ListingStatus) Enum.Parse(typeof(ListingStatus), request.Listing.ListingStatus);
            _context.Listings.Update(request.Listing);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
