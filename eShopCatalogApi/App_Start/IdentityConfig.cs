using eShopCatalogApi.DAL;
using eShopCatalogApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eShopCatalogApi.App_Start
{
    public static class IdentityConfig
    {
        public static UserManager<ApiUser> CreateUserManager(CatalogContext context)
        {
            var store = new UserStore<ApiUser>(context) { DisposeContext = false };
            var manager = new UserManager<ApiUser>(store);
            manager.UserValidator = new UserValidator<ApiUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            manager.PasswordValidator = new PasswordValidator { RequiredLength = 6 };
            return manager;
        }
    }
}
