using System;
using System.Data.Entity;
using eShopCatalogApi.App_Start;
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
            using (var manager = IdentityConfig.CreateUserManager(context))
            {
                var result = manager.Create(new ApiUser
                {
                    UserName = settings.SeedUserName
                }, settings.SeedPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Could not create the seed user: " + string.Join("; ", result.Errors));
                }
            }
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
