using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAllActiveListings
{
    public class GetAllActiveListingsQueryHandler : IRequestHandler<GetAllActiveListingsQuery, List<Domain.Entities.Listing>>
    {
        private readonly IMediator _mediator;

        public GetAllActiveListingsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<Domain.Entities.Listing>> Handle(GetAllActiveListingsQuery request, CancellationToken cancellationToken)
        {
            var allListings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            return allListings.Where(l => l.Status == ListingStatus.Listed || l.Status == ListingStatus.ComingSoon).ToList();
        }
    }
}
