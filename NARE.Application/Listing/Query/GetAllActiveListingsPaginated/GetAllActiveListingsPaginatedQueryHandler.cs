using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllActiveListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAllActiveListingsPaginated
{
    public class GetAllActiveListingsPaginatedQueryHandler : IRequestHandler<GetAllActiveListingsPaginatedQuery, PaginatedListingsDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllActiveListingsPaginatedQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedListingsDto> Handle(GetAllActiveListingsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var allActiveListings = await _mediator.Send(new GetAllActiveListingsQuery(), cancellationToken);
            var orderedListings = allActiveListings
                .OrderBy(l => l.Address);
            request.PaginationModel.Count = orderedListings.Count();
            var paginatedListings = orderedListings
                .Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();
            paginatedListings.ForEach(l =>
            {
                l.AgentDto = _mapper.Map<Domain.Entities.Agent, AgentDto>(l.Agent);
                l.Agent = null;
                l.ListingStatus = l.Status.ToString();
            });
            return await _mediator.Send(new GetPaginatedListingsResultQuery(listings: paginatedListings, paginationModel: request.PaginationModel), cancellationToken);
        }
    }
}
