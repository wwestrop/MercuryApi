using System.Collections.Generic;

namespace MercuryApi.Sample.Entities 
{
	public class Basket
	{
		public int Id { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
