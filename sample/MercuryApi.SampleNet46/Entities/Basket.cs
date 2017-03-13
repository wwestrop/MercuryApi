using System.Collections.Generic;

namespace MercuryApi.SampleNet46.Entities 
{
	public class Basket
	{
		public int Id { get; set; }

		public Customer Customer { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
