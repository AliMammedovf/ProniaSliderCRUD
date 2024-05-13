using Microsoft.AspNetCore.Mvc;
using ProniaFullPage.Business.Abstract;
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

            _categoryService.AddAsyncCategory(category);
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
            _categoryService.DeleteCategory(id);

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
        public IActionResult Update(int id, Category category)
        {
            if(!ModelState.IsValid)
                return View();

            _categoryService.UpdateCategory(id, category);
            return RedirectToAction("Index");
        }

        
    }
}
