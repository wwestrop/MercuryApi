using System.Collections.Generic;

namespace MercuryApi.Entities
{
    public class Product
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public IEnumerable<Review> Reviews { get; set; }

        public Seller Seller { get; set; }
    }
}
