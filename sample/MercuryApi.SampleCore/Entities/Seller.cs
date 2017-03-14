using System.Collections.Generic;

namespace MercuryApi.Sample.Entities
{
	public class Seller
	{
		public int Id { get; set; }

		public Address Address { get; set; }

		public string Name { get; set; }

		public ICollection<ProductSeller> Products { get; set; }

		public override string ToString() => this.Name;
	}
}
