using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Query.GetListingById;
using NARE.Domain.Exceptions.Listing;
using NARE.Persistence;

namespace NARE.Application.Listing.Command.RemoveListing
{
    public class RemoveListingCommandHandler : IRequestHandler<RemoveListingCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public RemoveListingCommandHandler(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<bool> Handle(RemoveListingCommand request, CancellationToken cancellationToken)
        {
            var listing = await _mediator.Send(new GetListingByIdQuery(Guid.Parse(request.ListingId)), cancellationToken);
            if (listing == null)
            {
                throw new InvalidListingException();
            }

            _context.Remove(listing);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
