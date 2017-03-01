using System.Collections.Generic;

namespace MercuryApi.Sample.Entities
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public ICollection<Review> Reviews { get; set; }

		public Seller Seller { get; set; }

		public override string ToString() => this.Name;
	}
}
