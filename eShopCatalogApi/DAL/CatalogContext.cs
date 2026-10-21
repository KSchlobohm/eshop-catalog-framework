using System.Data.Entity;
using eShopCatalogApi.Models;

namespace eShopCatalogApi.DAL
{
    public class CatalogContext : DbContext
    {
        public CatalogContext() : base("name=CatalogContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApiUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}
