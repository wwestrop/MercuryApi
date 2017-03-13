namespace MercuryApi.SampleNet46.Entities
{
	public class Seller
	{
		public int Id { get; set; }

		public Address Address { get; set; }

		public string Name { get; set; }

		public override string ToString() => this.Name;
	}
}
