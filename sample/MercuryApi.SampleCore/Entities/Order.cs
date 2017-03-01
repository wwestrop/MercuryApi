using System;
using System.Collections.Generic;

namespace MercuryApi.Sample.Entities 
{
	public class Order
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public Customer Customer { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
