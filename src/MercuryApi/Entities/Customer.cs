using System.Collections.Generic;

namespace MercuryApi.Entities
{
    public class Customer
    {
        public Address Address { get; set; }

        public Basket Basket { get; set; }

        public string Name { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
