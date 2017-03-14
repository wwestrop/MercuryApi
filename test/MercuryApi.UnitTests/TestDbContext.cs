using MercuryApi.UnitTests.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MercuryApi.UnitTests
{
	internal class TestDbContext : MercuryDbContext {

		internal TestDbContext(
			DbContextOptions options,
			IHttpContextAccessor httpContextAccess,
			IQueryParser httpQueryParser, 
			INavigationPathBuilder navPathBuilder,
			IExpressionApplicator expressionApplicator)
			
		: base(
			  options,
			  httpContextAccess, 
			  httpQueryParser,
			  navPathBuilder,
			  expressionApplicator) {
		}

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Manufacturer> Manufacturers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<Seller> Sellers { get; set; }
	}
}
