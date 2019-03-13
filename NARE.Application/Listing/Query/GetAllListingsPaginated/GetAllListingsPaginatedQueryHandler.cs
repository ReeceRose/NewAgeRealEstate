using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;

namespace NARE.Application.Listing.Query.GetAllListingsPaginated
{
    public class GetAllListingsPaginatedQueryHandler : IRequestHandler<GetAllListingsPaginatedQuery, PaginatedListingsDto>
    {
        private readonly IMediator _mediator;

        public GetAllListingsPaginatedQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PaginatedListingsDto> Handle(GetAllListingsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var allListings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            var orderedListings = allListings
                .OrderBy(l => l.Address);
            request.PaginationModel.Count = orderedListings.Count();
            var paginatedListings = orderedListings
                .Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();
            return await _mediator.Send(new GetPaginatedListingsResultQuery(listings: paginatedListings, paginationModel: request.PaginationModel), cancellationToken);
        }
    }
}
