using System;

namespace MercuryApi.Sample.Entities
{
	public class Review
	{
		public int Id { get; set; }

		public string Body { get; set; }

		public Customer Customer { get; set; }

		public Product Product { get; set; }

		public int Rating { get; set; }

		public override string ToString() => $"[{this.Customer?.Name}] ({this.Rating} stars) {this.Body.Substring(0, Math.Min(this.Body.Length, 60))}";
	}
}
