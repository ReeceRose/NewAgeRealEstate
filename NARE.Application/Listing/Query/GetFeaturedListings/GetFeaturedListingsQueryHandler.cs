using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Query.GetAllActiveListings;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetFeaturedListings
{
    public class GetFeaturedListingsQueryHandler : IRequestHandler<GetFeaturedListingsQuery, List<Domain.Entities.Listing>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetFeaturedListingsQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<Domain.Entities.Listing>> Handle(GetFeaturedListingsQuery request, CancellationToken cancellationToken)
        {
            var allListings = await _mediator.Send(new GetAllActiveListingsQuery(), cancellationToken);
            var featuredListings = allListings
                .OrderBy(l => l.ListingDate)
                .Where(l => l.Featured)
                .Take(request.Count)
                .ToList();

            featuredListings.ForEach(l =>
            {
                l.AgentDto = _mapper.Map<Domain.Entities.Agent, AgentDto>(l.Agent);
                l.Agent = null;
                l.ListingStatus = l.Status.ToString();
            });
            return featuredListings;
        }
    }
}
