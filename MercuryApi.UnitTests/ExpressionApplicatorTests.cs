using MercuryApi.Entities;
using System;
using Xunit;

namespace MercuryApi.UnitTests
{
    public class ExpressionApplicatorTests
    {
        private readonly ExpressionApplicator sut;

        public ExpressionApplicatorTests () {
            sut = new ExpressionApplicator();
        }

        // Avoid "backfilling" (e.g. if including "basket" and "basket.products", only need one .Include ---- if any earlier paths .beginWith() this path, exclude it)

        // Branching navigation  --- i.e. includes for a.b.c.d but also a.b.c.e.f.g (note we forked off after 'c')

        // Empty list of include requests

        // List containing duplicates

        // Handle HTTP-encoded separators

        // Correctly breaks down Include and ThenInclude

        // Include entities only as many times as needed? (Unless sub-entities require that navigation-property to be walked through first, in which case, start with the deepest paths?)

        // Handle dotted include paths

        // Handle multiple sub-includes off the same sub-entity

        // Handle non-collections

        // Handle collections, loading all x's of a given navProperty of a list of related entities

        // Handle case-insensitivity

        // Handle simple case


        [Fact]
        public void Test1()
        {
            sut.BuildNavigationPath(typeof(Customer), new[] { "Orders", "products", "manufacturer", "address" });

            sut.Build("basket,basket.products");

            //db.Set<Customer>().Include(c => c.Orders).ThenInclude(o => o.Products).ThenInclude(p => p.Manufacturer).ThenInclude(m => m.Address);
            sut.Build("Orders, Orders.products, Orders.products.mfgr, Orders.products.mfgr.address, Orders.products.reviews");
        }
    }
}
