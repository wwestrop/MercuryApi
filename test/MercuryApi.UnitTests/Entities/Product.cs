﻿using System.Collections.Generic;

namespace MercuryApi.UnitTests.Entities
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Manufacturer Manufacturer { get; set; }

		public IEnumerable<Review> Reviews { get; set; }

		public Seller Seller { get; set; }
	}
}
