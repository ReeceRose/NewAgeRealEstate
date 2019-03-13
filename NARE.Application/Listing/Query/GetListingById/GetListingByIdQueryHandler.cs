using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NARE.Application.Listing.Query.GetAllListings;
using NARE.Domain.Entities;
using NARE.Domain.Exceptions.Listing;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, Domain.Entities.Listing>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetListingByIdQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Listing> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
        {
            var listings = await _mediator.Send(new GetAllListingsQuery(), cancellationToken);
            var listing = listings.FirstOrDefault(l => l.Id == request.Id);
            if (listing == null) throw new InvalidListingException();
            listing.AgentDto = _mapper.Map<Domain.Entities.Agent, AgentDto>(listing.Agent);
            listing.Agent = null;
            listing.ListingStatus = listing.Status.ToString();
            return listing;
        }
    }
}
