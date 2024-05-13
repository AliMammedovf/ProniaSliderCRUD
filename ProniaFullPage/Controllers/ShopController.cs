using Microsoft.AspNetCore.Mvc;

namespace ProniaFullPage.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
