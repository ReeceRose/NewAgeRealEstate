using System.Collections.Generic;
using MediatR;

namespace NARE.Application.Listing.Query.GetNewestListings
{
    public class GetNewestListingsQuery : IRequest<List<Domain.Entities.Listing>>
    {
        public GetNewestListingsQuery(int? count) => Count = count ?? 3;
        public int Count { get; set; }
    }
}
