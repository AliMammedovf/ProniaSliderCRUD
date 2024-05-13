using ProniaFullPage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Abstract
{
    public interface ICategoryService
    {
        Task AddAsyncCategory(Category category);

        void DeleteCategory(int id);

        void UpdateCategory(int id, Category category);

        Category GetCategory(Func<Category, bool>? func = null);

        List<Category> GetAllCategories(Func<Category, bool>? func = null);
    }
}
