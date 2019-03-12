using System.Collections.Generic;
using MediatR;

namespace NARE.Application.Listing.Query.GetAllListings
{
    public class GetAllListingsQuery : IRequest<List<Domain.Entities.Listing>>
    {

    }
}
