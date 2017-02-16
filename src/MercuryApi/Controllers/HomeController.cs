using Microsoft.AspNetCore.Mvc;

namespace MercuryApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly RequestScopedStorage _rss; 

        public HomeController(RequestScopedStorage rss) {
            this._rss = rss;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            // HttpContext

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
