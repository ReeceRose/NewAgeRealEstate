using System;

namespace NARE.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Alternative { get; set; }
    }
}
