using System;
using System.Collections.Generic;

namespace NARE.Domain.Entities
{
    public class Listing
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public double AskingPrice { get; set; }
        public int BedroomCount { get; set; }
        public double BathroomCount { get; set; }
        public double GarageSize { get; set; }
        public int SquareFeet { get; set; }
        public double LotSize { get; set; }
        public int YearBuilt { get; set; }
        public DateTime ListingDate { get; set; }
        public string Description { get; set; }
        public Agent Agent { get; set; }
        public virtual IEnumerable<Image> Images { get; set; }
    }
}
