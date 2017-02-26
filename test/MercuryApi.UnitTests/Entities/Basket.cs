using System.Collections.Generic;

namespace MercuryApi.UnitTests.Entities
{
	public class Basket
	{
		public int Id { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
