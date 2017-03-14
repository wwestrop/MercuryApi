namespace MercuryApi.Sample.Entities 
{
	// Link entity (EF core does not support many-many relations natively without this)
	public class ProductSeller
	{
		public int Id { get; set; }

		public Product Product { get; set; }

		public Seller Seller { get; set; }
	}
}
