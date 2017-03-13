using MercuryApi.SampleNet46.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MercuryApi.SampleNet46
{
	public class SampleDbContext : MercuryDbContext {

		public SampleDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccess)
			: base(options, httpContextAccess) {
		}

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Basket> Baskets { get; set; }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Manufacturer> Manufacturers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<Seller> Sellers { get; set; }
	}
}
