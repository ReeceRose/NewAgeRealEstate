using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NARE.Application.Listing.Model;

namespace NARE.Application.Listing.Query.GetPaginatedListingsResult
{
    public class GetPaginatedListingsResultQueryHandler : IRequestHandler<GetPaginatedListingsResultQuery, PaginatedListingsDto>
    {
        public async Task<PaginatedListingsDto> Handle(GetPaginatedListingsResultQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PaginatedListingsDto()
            {
                PaginationModel = request.PaginationModel,
                Listings = request.Listings
            });
        }
    }
}
