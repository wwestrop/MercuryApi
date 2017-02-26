﻿using System.Collections.Generic;

namespace MercuryApi.UnitTests.Entities
{
	public class Customer
	{
		public int Id { get; set; }

		public Address Address { get; set; }

		public Basket Basket { get; set; }

		public string Name { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
