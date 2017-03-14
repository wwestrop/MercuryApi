namespace MercuryApi.SampleNet46.Entities 
{
	// Link entity (EF core does not support many-many relations natively without this)
	public class OrderProduct
	{
		public int Id { get; set; }

		public Order Order { get; set; }

		public Product Product { get; set; }
	}
}
