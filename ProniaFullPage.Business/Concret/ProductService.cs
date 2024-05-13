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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task AddAsyncProduct(Product product)
        {
            if (! _productRepository.GetAll().Any(x => x.Name == product.Name))
                await _productRepository.AddAsync(product);
            else
            {
                throw new DublicateException("Eyni ad daxil edile bilmez");
            }
            
           await _productRepository.CommitAsync();
        }

        public void DeleteProduct(int id)
        {
            var exsist= _productRepository.Get(x=> x.Id == id);
            if (exsist == null) throw new NullReferenceException();

            _productRepository.Delete(exsist);
            _productRepository.Commit();
        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            return _productRepository.GetAll();
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            return GetProduct(func);
        }

        public void UpdateProduct(int id, Product newProduct)
        {
            var oldProduct= _productRepository.Get(x=> x.Id == id);

            if(oldProduct == null) throw new NullReferenceException();

            if(! _productRepository.GetAll().Any(x=> x.Name == newProduct.Name))
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.Description = newProduct.Description;
                oldProduct.Price = newProduct.Price;
                oldProduct.ImageURL = newProduct.ImageURL;
                oldProduct.ImageFile = newProduct.ImageFile;
                oldProduct.CategoryId = newProduct.CategoryId;
            }
            else
            {
                throw new DublicateException("Eyni adli product ola bilmez!");
            }

            _productRepository.Commit();
        }
    }
}
