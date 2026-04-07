using Microsoft.AspNetCore.Mvc;

namespace SV22T1020142.Shop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string searchValue = "", int categoryID = 0, decimal? minPrice = null, decimal? maxPrice = null, int page = 1)
        {
            return RedirectToAction("Index", "Product", new { searchValue, categoryID, minPrice, maxPrice, page });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel());
        }
    }
}
