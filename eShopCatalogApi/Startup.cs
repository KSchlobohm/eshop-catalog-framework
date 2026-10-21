using System;
using System.Data.Entity;
using System.Web.Http;
using eShopCatalogApi.App_Start;
using eShopCatalogApi.DAL;
using eShopCatalogApi.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using Owin;

[assembly: OwinStartup(typeof(eShopCatalogApi.Startup))]

namespace eShopCatalogApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var settings = AuthenticationSettings.Load();
            Database.SetInitializer(new DatabaseInitializer(settings));
            using (var db = new CatalogContext())
            {
                db.Database.Initialize(false);
            }

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = settings.SigningKey,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    ClockSkew = TimeSpan.FromSeconds(30)
                }
            });

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
