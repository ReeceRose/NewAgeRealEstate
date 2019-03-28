using System;
using MediatR;

namespace NARE.Application.Listing.Query.GetListingById
{
    public class GetListingByIdQuery : IRequest<Domain.Entities.Listing>
    {
        public GetListingByIdQuery(Guid id) => Id = id;

        public Guid Id { get; set; }
    }
}
