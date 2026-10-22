using System.Data.Entity;
using eShopCatalogApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eShopCatalogApi.DAL
{
    public class CatalogContext : IdentityDbContext<ApiUser>
    {
        public CatalogContext() : base("name=CatalogContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}
