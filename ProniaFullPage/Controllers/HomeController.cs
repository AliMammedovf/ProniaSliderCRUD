using Microsoft.AspNetCore.Mvc;
using ProniaFullPage.Business.Abstract;
using System.Diagnostics;

namespace ProniaFullPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureService _featureService;

        public HomeController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public IActionResult Index()
        {
            var feature= _featureService.GetAllFeatures();
            return View(feature);
        }

       
    }
}
