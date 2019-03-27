using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Model;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Application.Listing.Query.GetPaginatedListingsResult;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAgentListings
{
    public class GetAgentListingsQueryHandler : IRequestHandler<GetAgentListingsQuery, PaginatedListingsDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAgentListingsQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PaginatedListingsDto> Handle(GetAgentListingsQuery request, CancellationToken cancellationToken)
        {
            var allListings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            var orderedListings = allListings
                .Where(l => l.Agent.Id == request.AgentId)
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
