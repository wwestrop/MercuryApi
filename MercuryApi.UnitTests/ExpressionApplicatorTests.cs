using MercuryApi.Entities;
using System;
using Xunit;

namespace MercuryApi.UnitTests
{
    public class ExpressionApplicatorTests
    {
        private readonly Applicator sut;

        public ExpressionApplicatorTests () {
            sut = new Applicator();
        }

        // Avoid "backfilling" (e.g. if including "basket" and "basket.products", only need one .Include ---- if any earlier paths .beginWith() this path, exclude it)

        // Branching navigation  --- i.e. includes for a.b.c.d but also a.b.c.e.f.g (note we forked off after 'c')

        // Include entities only as many times as needed? (Unless sub-entities require that navigation-property to be walked through first, in which case, start with the deepest paths?)

        // Handle multiple sub-includes off the same sub-entity

        // x Empty list of include requests

        // x List containing duplicates

        // x Handle HTTP-encoded separators

        // Correctly breaks down Include and ThenInclude        

        // x Handle dotted include paths

        // Handle non-collections

        // Handle collections, loading all x's of a given navProperty of a list of related entities

        // Handle case-insensitivity

        // x Handle simple case


        [Fact]
        public void Test1()
        {
            var res = new NavigationPathBuilder().Build(typeof(Customer), new[] { "Orders", "products", "manufacturer", "address" });

            //sut.Build("basket,basket.products");

            //db.Set<Customer>().Include(c => c.Orders).ThenInclude(o => o.Products).ThenInclude(p => p.Manufacturer).ThenInclude(m => m.Address);
            //sut.Build("Orders, Orders.products, Orders.products.mfgr, Orders.products.mfgr.address, Orders.products.reviews");
        }
    }
}
