using System.Collections.Generic;
using MediatR;
using NARE.Application.Listing.Model;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Query.GetPaginatedListingsResult
{
    public class GetPaginatedListingsResultQuery : IRequest<PaginatedListingsDto>
    {
        public GetPaginatedListingsResultQuery(List<Domain.Entities.Listing> listings, PaginationModel paginationModel)
        {
            Listings = listings;
            PaginationModel = paginationModel;
        }

        public List<Domain.Entities.Listing> Listings { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
