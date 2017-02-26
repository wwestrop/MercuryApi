using MercuryApi.Exceptions;
using MercuryApi.UnitTests.Entities;
using Xunit;

namespace MercuryApi.UnitTests
{
	public class NavigationPathBuilderTests
	{
		private readonly NavigationPathBuilder _sut;

		public NavigationPathBuilderTests() {
			_sut = new NavigationPathBuilder();
		}

		[Fact]
		public void Builds_Simple_NavigationPath() {

			// Arrange
			var queryParseResult = new[] { "Seller", "Address" };

			// Act
			var result = _sut.Build(typeof(Product), queryParseResult);

			// Assert
			Assert.Equal("Seller.Address", result);
		}

		[Fact]
		public void Builds_NavigationPath_Over_A_Collection() {

			// Arrange
			var queryParseResult = new[] { "Orders", "Products", "Manufacturer", "Address" };

			// Act
			var result = _sut.Build(typeof(Customer), queryParseResult);

			// Assert
			Assert.Equal("Orders.Products.Manufacturer.Address", result);
		}

		[Fact]
		public void Corrects_Capitalisation_For_Simple_NavigationPath() {

			// Arrange
			var queryParseResult = new[] { "seller", "adDReSs" };

			// Act
			var result = _sut.Build(typeof(Product), queryParseResult);

			// Assert
			Assert.Equal("Seller.Address", result);
		}

		[Fact]
		public void Corrects_Capitalisation_For_Navigation_Over_A_Collection() {

			// Arrange
			var queryParseResult = new[] { "Orders", "products", "manufacturer", "ADdREss" };

			// Act
			var result = _sut.Build(typeof(Customer), queryParseResult);

			// Assert
			Assert.Equal("Orders.Products.Manufacturer.Address", result);
		}

		[Fact]
		public void Incorrect_NavigationProperty_Throws_Helpful_Exception() {

			// Arrange
			var queryParseResult = new[] { "wishlist" };

			// Act
			InvalidNavigationException thrownException = null;
			try {
				var result = _sut.Build(typeof(Customer), queryParseResult);
			}
			catch(InvalidNavigationException e) {
				thrownException = e;
			}

			// Assert
			Assert.NotNull(thrownException);
			Assert.True(thrownException.Message.Contains("wishlist")); // The exception explicitly names the offending queryParameter
		}

		[Fact]
		public void Incorrect_NavigationProperty_Over_A_Collection_Throws_Helpful_Exception() {

			// Arrange
			var queryParseResult = new[] { "orders", "trackingInformation" };

			// Act
			InvalidNavigationException thrownException = null;
			try {
				var result = _sut.Build(typeof(Customer), queryParseResult);
			}
			catch (InvalidNavigationException e) {
				thrownException = e;
			}

			// Assert
			Assert.NotNull(thrownException);
			Assert.True(thrownException.Message.Contains("trackingInformation")); // The exception explicitly names the offending queryParameter
		}

		[Fact]
		public void Ambiguous_NavigationProperty_Throws_Exception() {

			// Arrange
			var queryParseResult = new[] { "FOO" };

			// Act & Assert
			Assert.Throws<AmbiguousNavigationException>(() => _sut.Build(typeof(AmbiguousEntity), queryParseResult));
		}
	}
}
