using MercuryApi.Sample.Entities;
using Microsoft.AspNetCore.Mvc;

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
			Address result = null;
            return Json(result);
        }

		[Route("/basket")]
		public IActionResult Basket() {
			Basket result = null;
			return Json(result);
		}

		[Route("/customer")]
		public IActionResult Customer() {
			Customer result = null;
			return Json(result);
		}

		[Route("/manufacturer")]
		public IActionResult Manufacturer() {
			Manufacturer result = null;
			return Json(result);
		}

		[Route("/order")]
		public IActionResult Order() {
			Order result = null;
			return Json(result);
		}

		[Route("/prodct")]
		public IActionResult Product() {
			Product result = null;
			return Json(result);
		}

		[Route("/review")]
		public IActionResult Review() {
			Review result = null;
			return Json(result);
		}

		[Route("/seller")]
		public IActionResult Seller() {
			Seller result = null;
			return Json(result);
		}
	}
}
