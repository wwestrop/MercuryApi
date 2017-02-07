using System.Collections.Generic;

namespace MercuryApi.Entities
{
    public class Basket
    {
        public ICollection<Product> Products { get; set; }
    }
}
