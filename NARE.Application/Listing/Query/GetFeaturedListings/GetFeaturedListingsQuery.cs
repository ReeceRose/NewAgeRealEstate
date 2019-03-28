using System.Collections.Generic;
using MediatR;

namespace NARE.Application.Listing.Query.GetFeaturedListings
{
    public class GetFeaturedListingsQuery : IRequest<List<Domain.Entities.Listing>>
    {
        public GetFeaturedListingsQuery(int? count) => Count = count ?? 3;
        public int Count { get; set; }
    }
}
