using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Query.GetAllListings;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, Domain.Entities.Listing>
    {
        private readonly IMediator _mediator;

        public GetListingByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Domain.Entities.Listing> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
        {
            var listings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            return listings.FirstOrDefault(l => l.Id == request.Id);
        }
    }
}
