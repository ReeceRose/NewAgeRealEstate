using MediatR;
using NARE.Application.Listing.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetAllActiveListingsPaginated
{
    public class GetAllActiveListingsPaginatedQuery : IRequest<PaginatedListingsDto>
    {
        public GetAllActiveListingsPaginatedQuery(PaginationModel paginationModel) => PaginationModel = paginationModel;

        public PaginationModel PaginationModel { get; set; }
    }
}
