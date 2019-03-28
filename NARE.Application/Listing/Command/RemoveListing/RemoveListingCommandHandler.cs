using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NARE.Application.Listing.Query.GetListingById;
using NARE.Domain.Exceptions.Listing;
using NARE.Persistence;

namespace NARE.Application.Listing.Command.RemoveListing
{
    public class RemoveListingCommandHandler : IRequestHandler<RemoveListingCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RemoveListingCommandHandler> _logger;

        public RemoveListingCommandHandler(IMediator mediator, ApplicationDbContext context, ILogger<RemoveListingCommandHandler> logger)
        {
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(RemoveListingCommand request, CancellationToken cancellationToken)
        {
            var listing = await _mediator.Send(new GetListingByIdQuery(Guid.Parse(request.ListingId)), cancellationToken);
            if (listing == null)
            {
                _logger.LogInformation($"Remove Listing: {request.ListingId}: Failed removal: Listing ID does not exist");
                throw new InvalidListingException();
            }

            _context.Remove(listing);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Remove Listing: {request.ListingId}: Listing successfully removed");
            return await Task.FromResult(true);
        }
    }
}
