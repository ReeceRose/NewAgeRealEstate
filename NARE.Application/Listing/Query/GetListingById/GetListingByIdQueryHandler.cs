using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, Domain.Entities.Listing>
    {
        public Task<Domain.Entities.Listing> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
