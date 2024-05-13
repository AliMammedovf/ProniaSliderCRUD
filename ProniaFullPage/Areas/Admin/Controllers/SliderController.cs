using Microsoft.AspNetCore.Mvc;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public IActionResult Index()
        {
            var slider = _sliderService.GetAllSliders();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        { 

             await _sliderService.AddAsyncSlider(slider);
             return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var exsist = _sliderService.GetSlider(x => x.Id == id);
            if(exsist == null)
            {
                return NotFound();
            }
            return View(exsist);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            _sliderService.DeleteSlider(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var oldSlider= _sliderService.GetSlider(x=>x.Id==id);
            if(oldSlider == null)
            {
                return NotFound();
            }
            return View(oldSlider);
        }

        [HttpPost]
        public IActionResult Update(int id, Slider newSlider)
        {
            if (!ModelState.IsValid)
                return View();

            _sliderService.UpdateSlider(id, newSlider);
            return RedirectToAction("Index");
        }

    }
}
