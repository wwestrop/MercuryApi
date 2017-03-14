namespace MercuryApi.UnitTests.Entities
{
	public class Address
	{
		public int Id { get; set; }

		public string Line1 { get; set; }

		public string Line2 { get; set; }

		public string Postcode { get; set; }

		public override string ToString() => $"{Line1}, {Line2}, {Postcode}";

	}
}
