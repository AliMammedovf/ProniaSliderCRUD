using Microsoft.AspNetCore.Mvc;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.ViewModels;
using System.Diagnostics;

namespace ProniaFullPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly ISliderService _sliderService;

        public HomeController(IFeatureService featureService, ISliderService sliderService)
        {
            _featureService = featureService;
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var feature= _featureService.GetAllFeatures();
            var slider= _sliderService.GetAllSliders();
            HomeVM vm = new HomeVM()
            {
               Features = feature,
               Sliders = slider,
            };
            return View(vm);
        }

       
    }
}
