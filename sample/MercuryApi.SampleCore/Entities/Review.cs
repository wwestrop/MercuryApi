using System;

namespace MercuryApi.Sample.Entities
{
	public class Review
	{
		public int Id { get; set; }

		public string Body { get; set; }

		public int Rating { get; set; }

		public override string ToString() => $"{this.Rating} : {this.Body.Substring(0, Math.Min(this.Body.Length, 60))}";
	}
}
