using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.ViewService;

public class ShopCategoryService
{
    private readonly ICategoryService _categoryService;

    public ShopCategoryService(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public List<Category> GetCategories()
    {
        return _categoryService.GetAllCategories();
    }
}
