using System;
using System.Collections.Generic;

namespace MercuryApi.Sample.Entities 
{
	public class Order
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public Address DeliverTo { get; set; }

		public IEnumerable<Product> Products { get; set; }
	}
}
