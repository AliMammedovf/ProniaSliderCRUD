using ProniaFullPage.Core.Models;
using ProniaFullPage.Core.RepositoryAbstract;
using ProniaFullPage.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Data.RepositoryCancret
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
