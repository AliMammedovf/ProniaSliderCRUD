using Microsoft.EntityFrameworkCore;
using ProniaFullPage.Core.Models;

namespace ProniaFullPage.Data.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }    
        public DbSet<Tag> Tags { get; set; }
        public  DbSet<Feature> Features { get; set; }

    }
}
