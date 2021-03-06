﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Query.GetAllListings;

namespace NARE.Application.Listing.Query.GetListingCount
{
    public class GetListingCountQueryHandler : IRequestHandler<GetListingCountQuery, int>
    {
        private readonly IMediator _mediator;

        public GetListingCountQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(GetListingCountQuery request, CancellationToken cancellationToken)
        {
            var listings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            if (request.AgentId == null) return listings?.Count ?? 0;
            return listings.Count(l => l.Agent.Id == request.AgentId);
        }
    }
}
