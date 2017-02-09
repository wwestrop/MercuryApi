using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MercuryApi.UnitTests
{
    public class QueryParserTests
    {

        private readonly QueryParser sut;

        public QueryParserTests() {
            sut = new QueryParser();
        }

        // multi-part includes (dotted)  

        [Fact]
        public void No_Query_Parameters_Provided() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
            });

            // Act
            var result = sut.Parse(queryString);

            // Assert
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void Include_Parameter_Not_Provided() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "someOtherQueryParameter", new StringValues("value") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            Assert.Equal(0, includePaths.Length);
        }

        /*[Fact]
        public void Multiple_Include_Parameters() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("value") },
                { "include", new StringValues("anotherValue") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(2, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            Assert.Equal("anotherValue", navProperty1);
            Assert.Equal("value", navProperty2);
        }*/

        [Fact]
        public void Duplicated_NavigationString_In_Same_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("manufacturer,seller,manufacturer") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(2, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            Assert.Equal("manufacturer", navProperty1);
            Assert.Equal("seller", navProperty2);
        }

        /*[Fact]
        public void Duplicated_NavigationString_In_Different_Parameters() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("manufacturer,seller") },
                { "referrer", new StringValues("http://abcdefg.co.uk") },
                { "include", new StringValues("manufacturer") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(2, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            Assert.Equal("manufacturer", navProperty1);
            Assert.Equal("seller", navProperty2);
        }*/

        [Fact]
        public void Duplicated_NavigationString_Differing_Only_In_Capitalisation() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("PropertyToLoad,propertyToLoad") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            Assert.Equal(1, includePaths.Length);

            var includePath = includePaths[0][0];
            Assert.True(includePath.Equals("PROPERTYtoLOAD", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Superfluous_Spaces_In_Include_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("employee,manager, company    ,     department        ,  headOffice") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(5, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            var navProperty3 = includePaths[2][0];
            var navProperty4 = includePaths[3][0];
            var navProperty5 = includePaths[4][0];
            Assert.Equal("company", navProperty1);
            Assert.Equal("department", navProperty2);
            Assert.Equal("employee", navProperty3);
            Assert.Equal("headOffice", navProperty4);
            Assert.Equal("manager", navProperty5);
        }
        
        [Fact]
        public void Empty_Values_In_Include_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues(",seat,price,,,,,,,   ,,,venue,    ,,openingActs,") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(4, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            var navProperty3 = includePaths[2][0];
            var navProperty4 = includePaths[3][0];
            Assert.Equal("openingActs", navProperty1);
            Assert.Equal("price", navProperty2);
            Assert.Equal("seat", navProperty3);
            Assert.Equal("venue", navProperty4);
        }

        [Fact]
        public void Http_Encoded_Include_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("documents,%2Ctags,+author") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(3, includePaths.Length);

            var navProperty1 = includePaths[0][0];
            var navProperty2 = includePaths[1][0];
            var navProperty3 = includePaths[2][0];
            Assert.Equal("author", navProperty1);
            Assert.Equal("documents", navProperty2);
            Assert.Equal("tags", navProperty3);
        }

        [Fact]
        public void Parses_Simple_Include_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("employee") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(1, includePaths.Length);
            var includePath = includePaths[0];
            Assert.Equal(1, includePath.Length);
            Assert.Equal("employee", includePath[0]);
        }

        [Fact]
        public void Parses_Dotted_Include_Parameter() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("employee.department") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(1, includePaths.Length);

            var includePath = includePaths[0];
            Assert.Equal(2, includePath.Length);
            var navProperty1_a = includePaths[0][0];
            var navProperty1_b = includePaths[0][1];

            Assert.Equal("employee", navProperty1_a);
            Assert.Equal("department", navProperty1_b);
        }

        [Fact]
        public void Parses_Multiple_Dotted_Include_Parameters() {

            // Arrange
            var queryString = new QueryCollection(new Dictionary<string, StringValues> {
                { "include", new StringValues("publisher.address,author.biography,documentBody") }
            });

            // Act
            var includePaths = sut.Parse(queryString);

            // Assert
            includePaths = includePaths.OrderBy(p => p[0]).ToArray();
            Assert.Equal(3, includePaths.Length);
            Assert.Equal(2, includePaths[0].Length);
            Assert.Equal(1, includePaths[1].Length);
            Assert.Equal(2, includePaths[2].Length);

            var navProperty1_a = includePaths[0][0];
            var navProperty1_b = includePaths[0][1];
            var navProperty2 = includePaths[1][0];
            var navProperty3_a = includePaths[2][0];
            var navProperty3_b = includePaths[2][1];

            Assert.Equal("author", navProperty1_a);
            Assert.Equal("biography", navProperty1_b);
            Assert.Equal("documentBody", navProperty2);
            Assert.Equal("publisher", navProperty3_a);
            Assert.Equal("address", navProperty3_b);
        }

    }
}
