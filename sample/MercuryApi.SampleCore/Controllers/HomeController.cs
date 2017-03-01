using MercuryApi.Sample.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MercuryApi.Sample.Controllers
{
    public class HomeController : Controller
    {
		private readonly SampleDbContext _dbCtxt;

		public HomeController(SampleDbContext dbCtxt) {
			this._dbCtxt = dbCtxt;
		}

        public IActionResult Index() {
            return View();
        }

		[Route("/address")]
        public IActionResult Address() {
			var result = this._dbCtxt.IncludedSet<Address>().ToList();
            return Json(result);
        }

		[Route("/basket")]
		public IActionResult Basket() {
			var result = this._dbCtxt.IncludedSet<Basket>().ToList();
			return Json(result);
		}

		[Route("/customer")]
		public IActionResult Customer() {
			var result = this._dbCtxt.IncludedSet<Customer>().ToList();
			return Json(result);
		}

		[Route("/manufacturer")]
		public IActionResult Manufacturer() {
			var result = this._dbCtxt.IncludedSet<Manufacturer>().ToList();
			return Json(result);
		}

		[Route("/order")]
		public IActionResult Order() {
			var result = this._dbCtxt.IncludedSet<Order>().ToList();
			return Json(result);
		}

		[Route("/product")]
		public IActionResult Product() {
			var result = this._dbCtxt.IncludedSet<Product>().ToList();
			return Json(result);
		}

		[Route("/review")]
		public IActionResult Review() {
			var result = this._dbCtxt.IncludedSet<Review>().ToList();
			return Json(result);
		}

		[Route("/seller")]
		public IActionResult Seller() {
			var result = this._dbCtxt.IncludedSet<Seller>().ToList();
			return Json(result);
		}
	}
}
