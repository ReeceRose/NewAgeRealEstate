using MediatR;

namespace NARE.Application.Listing.Command.CreateListing
{
    public class CreateListingCommand : IRequest<bool>
    {
        public CreateListingCommand(Domain.Entities.Listing listing) => Listing = listing;

        public Domain.Entities.Listing Listing { get; set; }
    }
}
