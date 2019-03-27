using MediatR;
using NARE.Application.Listing.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.SearchActiveListings
{
    public class SearchActiveListingsQuery : IRequest<PaginatedListingsDto>
    {
        public SearchActiveListingsQuery(PaginationModel paginationModel, SearchModel searchModel)
        {
            PaginationModel = paginationModel;
            SearchModel = searchModel;
        }

        public PaginationModel PaginationModel { get; set; }
        public SearchModel SearchModel { get; set; }
    }
}
