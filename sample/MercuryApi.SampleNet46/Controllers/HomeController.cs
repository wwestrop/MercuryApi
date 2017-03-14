using MercuryApi.SampleNet46.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace MercuryApi.SampleNet46.Controllers
{
	public class HomeController : Controller
	{
		private readonly SampleDbContext _dbCtxt;
		private readonly JsonSerializerSettings serialisationSettings = new JsonSerializerSettings {
			Formatting = Formatting.Indented,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};

		public HomeController(SampleDbContext dbCtxt) {
			this._dbCtxt = dbCtxt;
		}

		public IActionResult Index() {
			return View();
		}

		[Route("/address")]
		public IActionResult Address() {
			var result = this._dbCtxt.IncludedSet<Address>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/customer")]
		public IActionResult Customer() {
			var result = this._dbCtxt.IncludedSet<Customer>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/manufacturer")]
		public IActionResult Manufacturer() {
			var result = this._dbCtxt.IncludedSet<Manufacturer>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/order")]
		public IActionResult Order() {
			var result = this._dbCtxt.IncludedSet<Order>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/product")]
		public IActionResult Product() {
			var result = this._dbCtxt.IncludedSet<Product>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/review")]
		public IActionResult Review() {
			var result = this._dbCtxt.IncludedSet<Review>().ToList();
			return Json(result, serialisationSettings);
		}

		[Route("/seller")]
		public IActionResult Seller() {
			var result = this._dbCtxt.IncludedSet<Seller>().ToList();
			return Json(result, serialisationSettings);
		}
	}
}
