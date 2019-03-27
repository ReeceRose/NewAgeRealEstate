using System.Collections.Generic;
using MediatR;

namespace NARE.Application.Listing.Query.GetAllActiveListings
{
    public class GetAllActiveListingsQuery : IRequest<List<Domain.Entities.Listing>>
    {

    }
}
