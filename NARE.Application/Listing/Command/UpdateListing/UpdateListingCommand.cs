using MediatR;

namespace NARE.Application.Listing.Command.UpdateListing
{
    public class UpdateListingCommand : IRequest<bool>
    {
        public UpdateListingCommand(Domain.Entities.Listing listing) => Listing = listing;
        public Domain.Entities.Listing Listing { get; set; }
    }
}
