using MercuryApi.UnitTests.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MercuryApi.UnitTests
{
	/// <summary>
	/// The MercuryDbContext is what puts everything together
	/// </summary>
	public class MercuryDbContextTests {

		private readonly MercuryDbContext _sut;
		private readonly IExpressionApplicator _applicator;
		private readonly IQueryParser _httpQueryParser;
		private readonly IHttpContextAccessor _httpContextAccess;
		private readonly INavigationPathBuilder _navPathBuilder;

		public MercuryDbContextTests() {
			_httpContextAccess = Substitute.For<IHttpContextAccessor>();
			_httpQueryParser = new QueryParser();
			_applicator = Substitute.For<IExpressionApplicator>();
			_navPathBuilder = new NavigationPathBuilder();

			var optsBuilder = new DbContextOptionsBuilder<MercuryDbContext>()
				.UseInMemoryDatabase("TestDB");

			_sut = new TestDbContext(optsBuilder.Options, _httpContextAccess, _httpQueryParser, _navPathBuilder, _applicator);
		}

		[Fact]
		public void Multiple_Include_Paths() {

			// Arrange
			_httpContextAccess.HttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues> {
				{ "include", new StringValues(new [] {"orders.products", "address"}) },
			});

			// Act
			var result = _sut.IncludedSet<Customer>();

			// Assert
			_applicator.Received().Include(Arg.Any<IQueryable<Customer>>(), "Orders.Products");
			_applicator.Received().Include(Arg.Any<IQueryable<Customer>>(), "Address");
		}

		[Fact]
		public void No_Include_Paths() {

			// Arrange
			_httpContextAccess.HttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues>());

			// Act
			var result = _sut.IncludedSet<Customer>();

			// Assert
			_applicator.DidNotReceive().Include(Arg.Any<IQueryable<Customer>>(), Arg.Any<string>());
		}
	}
}
