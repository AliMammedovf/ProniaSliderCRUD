using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        
        private readonly IFeatureService _featureService;
        
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public IActionResult Index()
        {
            var feature= _featureService.GetAllFeatures();
            return View(feature);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {

                _featureService.AddAsyncFeature(feature);

            }
            catch (DublicateException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
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
            var exsist = _featureService.GetFeature(x => x.Id == id);

            if (exsist == null)
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
                _featureService.DeleteFeature(id);
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var oldFeature = _featureService.GetFeature(x=>x.Id == id);
            if (oldFeature == null)
            {
                return NotFound();
            }

            return View(oldFeature);
        }

        [HttpPost]
        public IActionResult Update(int id, Feature feature)
        {
            if(!ModelState.IsValid)
                return View();
            

            try
            {
                _featureService.UpdateFeature(id, feature);
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }

    }
}
