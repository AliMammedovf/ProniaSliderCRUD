using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
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
            if(!ModelState.IsValid)
                return View();

          
            try
            {

                await _sliderService.AddAsyncSlider(slider);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();

            }
            catch (FileNullReferanceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                

            }

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
            try
            {
                _sliderService.DeleteSlider(id);
            }
            catch (NotFoundIdException ex)
            {
                return NotFound();
            }
            catch(NotFoundFileException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
        public IActionResult Update(Slider newSlider)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _sliderService.UpdateSlider(newSlider.Id, newSlider);
            }
            catch(NullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch(ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch(ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch(FileNotFoundException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}
