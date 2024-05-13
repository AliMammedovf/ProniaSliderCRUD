using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;
using ProniaFullPage.Core.RepositoryAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Concret
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsyncCategory(Category category)
        {
            if(! _categoryRepository.GetAll().Any(x=>x.Id == category.Id))
                await _categoryRepository.AddAsync(category);
            else
            {
                throw new DublicateException("Eyni adli category ola bilmez!");
            }

            await _categoryRepository.CommitAsync();
        }

        public void DeleteCategory(int id)
        {
            var exsist=_categoryRepository.Get(x=>x.Id == id);

            if (exsist == null) throw new NullReferenceException();

            _categoryRepository.Delete(exsist);
            _categoryRepository.Commit();
        }

        public List<Category> GetAllCategories(Func<Category, bool>? func = null)
        {
            return _categoryRepository.GetAll(func);
        }

        public Category GetCategory(Func<Category, bool>? func = null)
        {
            return _categoryRepository.Get(func);
        }

        public void UpdateCategory(int id, Category newCategory)
        {
            var oldCategory= _categoryRepository.Get(x=> x.Id == id);

            if (oldCategory == null) throw new NullReferenceException();

            

            if(! _categoryRepository.GetAll().Any(x=>x.Name == newCategory.Name))
            {
                oldCategory.Name = newCategory.Name;
            }
            else
            {
                throw new DublicateException("Eyni adli category olmaz!");
            }

            _categoryRepository.Commit();
        }
    }
}
