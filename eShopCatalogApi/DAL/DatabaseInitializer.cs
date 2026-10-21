using System.Data.Entity;
using eShopCatalogApi.Models;
using eShopCatalogApi.Security;
using Microsoft.AspNet.Identity;

namespace eShopCatalogApi.DAL
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<CatalogContext>
    {
        private readonly AuthenticationSettings settings;

        public DatabaseInitializer(AuthenticationSettings settings)
        {
            this.settings = settings;
        }

        protected override void Seed(CatalogContext context)
        {
            context.Users.Add(new ApiUser
            {
                UserName = settings.SeedUserName,
                PasswordHash = new PasswordHasher().HashPassword(settings.SeedPassword)
            });
            context.Products.AddRange(new[]
            {
                new Product { Sku = "MUG-001", Name = "eShop Coffee Mug", Price = 12.50m },
                new Product { Sku = "TEE-001", Name = "eShop T-Shirt", Price = 24.00m },
                new Product { Sku = "BAG-001", Name = "eShop Tote Bag", Price = 18.00m }
            });
            context.SaveChanges();
        }
    }
}
