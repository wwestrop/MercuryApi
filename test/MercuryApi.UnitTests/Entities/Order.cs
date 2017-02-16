using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryApi.UnitTests.Entities
{
    public class Order
    {
        public DateTime Date { get; set; }

        public Address DeliverTo { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
