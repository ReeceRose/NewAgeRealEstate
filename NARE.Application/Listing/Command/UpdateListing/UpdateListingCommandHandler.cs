using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Domain.Entities;
using NARE.Persistence;
using Enum = System.Enum;

namespace NARE.Application.Listing.Command.UpdateListing
{
    public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateListingCommandHandler> _logger;

        public UpdateListingCommandHandler(ApplicationDbContext context, ILogger<UpdateListingCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            request.Listing.Status = (ListingStatus) Enum.Parse(typeof(ListingStatus), request.Listing.ListingStatus);
            _context.Listings.Update(request.Listing);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Listing Update: {request.Listing.Address}: Successfully updated");
            return await Task.FromResult(true);
        }
    }
}
