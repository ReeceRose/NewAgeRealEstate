using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetNewestListings
{
    public class GetNewestListingsQueryHandler : IRequestHandler<GetNewestListingsQuery, List<Domain.Entities.Listing>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetNewestListingsQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<Domain.Entities.Listing>> Handle(GetNewestListingsQuery request, CancellationToken cancellationToken)
        {
            var allListings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            var newestListings = allListings
                .OrderByDescending(l => l.ListingDate)
                .Take(request.Count)
                .ToList();

            newestListings.ForEach(l =>
            {
                l.AgentDto = _mapper.Map<Domain.Entities.Agent, AgentDto>(l.Agent);
                l.Agent = null;
                l.ListingStatus = l.Status.ToString();
            });
            return newestListings;
        }
    }
}
