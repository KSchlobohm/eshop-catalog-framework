using System.Web.Http;
using Swashbuckle.Application;

namespace eShopCatalogApi.App_Start
{
    public static class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "eShop Catalog API");
                c.ApiKey("Bearer")
                    .Description("Enter Bearer followed by the access_token returned by POST /api/auth/login.")
                    .Name("Authorization")
                    .In("header");
            })
            .EnableSwaggerUi(c => c.EnableApiKeySupport("Authorization", "header"));
        }
    }
}
