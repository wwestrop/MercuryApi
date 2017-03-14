using System.Collections.Generic;

namespace MercuryApi.UnitTests.Entities
{
	public class Customer
	{
		public int Id { get; set; }

		public Address Address { get; set; }

		public string Name { get; set; }

		public IEnumerable<Order> Orders { get; set; }

		public IEnumerable<Review> Reviews { get; set; }

		public override string ToString() => this.Name;
	}
}
