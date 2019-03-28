using System;

namespace NARE.Domain.Entities
{
    public class SearchModel
    {
        public string City { get; set; } = null;
        public string Keywords { get; set; } = null;
        public int? MinimumPrice { get; set; } = 0;
        public int? MaximumPrice { get; set; } = Int32.MaxValue;
        public int MinimumBedrooms { get; set; } = 0;
        public int MinimumBathrooms { get; set; } = 0;
    }
}
