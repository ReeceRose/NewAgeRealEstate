using MediatR;

namespace NARE.Application.Listing.Command.RemoveListing
{
    public class RemoveListingCommand : IRequest<bool>
    {
        public RemoveListingCommand(string listingId) => ListingId = listingId;
        public string ListingId { get; }
    }
}
