using System.Collections.Generic;

namespace MercuryApi.SampleNet46.Entities
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public ICollection<OrderProduct> Orders { get; set; }

		public decimal Price { get; set; }

		public ICollection<Review> Reviews { get; set; }

		public ICollection<ProductSeller> Sellers { get; set; }

		public override string ToString() => this.Name;
	}
}
