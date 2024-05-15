using Microsoft.AspNetCore.Mvc;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var category = _categoryService.GetAllCategories();
            return View(category);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _categoryService.AddAsyncCategory(category);
            }
            catch(DublicateException ex)
            {
                ModelState.AddModelError("Name",ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");   
        }

        public IActionResult Delete(int id)
        {
            var exsist= _categoryService.GetCategory(x=>x.Id==id);

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
                _categoryService.DeleteCategory(id);
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
            var oldCategory= _categoryService.GetCategory(x=> x.Id==id);
            if (oldCategory == null)
            {
                return NotFound();
            }
            return View(oldCategory);
        }

        [HttpPost]
        public IActionResult Update( Category category)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _categoryService.UpdateCategory(category.Id, category);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch(DublicateException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }

        
    }
}
