using ProniaFullPage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Abstract
{
    public interface IProductService
    {
        Task AddAsyncProduct(Product product);

        void DeleteProduct(int id); 

        void UpdateProduct(int id, Product product);    

        Product GetProduct(Func<Product, bool>? func=null);

        List<Product> GetAllProducts(Func<Product,bool>? func=null);

    }
}
