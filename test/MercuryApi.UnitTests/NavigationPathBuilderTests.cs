using MercuryApi.Exceptions;
using MercuryApi.UnitTests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.False(true);
        }

        [Fact]
        public void Builds_NavigationPath_Over_A_Collection() {

            // Arrange
            var queryParseResult = new[] { "Orders", "products", "manufacturer", "ADdREss" };

            // Act
            var result = _sut.Build(typeof(Customer), queryParseResult);

            // Assert
            Assert.False(true);
        }

        [Fact]
        public void Corrects_Capitalisation_For_Simple_NavigationPath() {

            // Arrange
            var queryParseResult = new[] { "seller", "adDReSs" };

            // Act
            var result = _sut.Build(typeof(Product), queryParseResult);

            // Assert
            Assert.False(true);
        }

        [Fact]
        public void Corrects_Capitalisation_For_Navigation_Over_A_Collection() {

            // Arrange

            // Act

            // Assert
            Assert.False(true);
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

        /*

        deals with collections with multiple generic types (dictionary? T is KVP)





        // x Include entities only as many times as needed? (Unless sub-entities require that navigation-property to be walked through first, in which case, start with the deepest paths?)

        // x Handle multiple sub-includes off the same sub-entity

        // x Empty list of include requests

        // x List containing duplicates

        // Correctly breaks down Include and ThenInclude        

        // Handle case-insensitivity

         */
    }
}
