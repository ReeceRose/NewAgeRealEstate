using System.Collections.Generic;
using NARE.Domain.Entities;

namespace NARE.Application.Listing.Model
{
    public class PaginatedListingsDto
    {
        public List<Domain.Entities.Listing> Listings { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
