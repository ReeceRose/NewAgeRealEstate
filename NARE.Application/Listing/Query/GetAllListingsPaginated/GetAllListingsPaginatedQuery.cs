using MediatR;
using NARE.Application.Listing.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAllListingsPaginated
{
    public class GetAllListingsPaginatedQuery : IRequest<PaginatedListingsDto>
    {
        public GetAllListingsPaginatedQuery(PaginationModel paginationModel) => PaginationModel = paginationModel;

        public PaginationModel PaginationModel { get; set; }
    }
}
