using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllActiveListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.SearchActiveListings
{
    public class SearchActiveListingsQueryHandler : IRequestHandler<SearchActiveListingsQuery, PaginatedListingsDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchActiveListingsQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedListingsDto> Handle(SearchActiveListingsQuery request, CancellationToken cancellationToken)
        {
            var listings = await _mediator.Send(new GetAllActiveListingsQuery(), cancellationToken);
            var filteredListings = listings
                .Where(l => request.SearchModel.City == null || l.City.ToLower().Contains(request.SearchModel.City.ToLower()))
                .Where(l => request.SearchModel.MinimumPrice == null || l.AskingPrice >= request.SearchModel.MinimumPrice)
                .Where(l => request.SearchModel.MaximumPrice == null || l.AskingPrice <= request.SearchModel.MaximumPrice)
                .Where(l => l.BedroomCount >= request.SearchModel.MinimumBedrooms)
                .Where(l => l.BathroomCount >= request.SearchModel.MinimumBathrooms)
                .Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();

            filteredListings.ForEach(l =>
            {
                l.AgentDto = _mapper.Map<Domain.Entities.Agent, AgentDto>(l.Agent);
                l.Agent = null;
                l.ListingStatus = l.Status.ToString();
            });
            return await _mediator.Send(new GetPaginatedListingsResultQuery(listings: filteredListings, paginationModel: request.PaginationModel), cancellationToken);
        }
    }
}
