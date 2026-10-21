using System;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using eShopCatalogApi.DAL;
using eShopCatalogApi.Models;
using eShopCatalogApi.Security;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eShopCatalogApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly CatalogContext db = new CatalogContext();

        [AllowAnonymous, HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            if (request == null)
                return BadRequest("A login request is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await db.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName);
            var hasher = new PasswordHasher();
            if (user == null || hasher.VerifyHashedPassword(user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var settings = AuthenticationSettings.Load();
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                },
                notBefore: now,
                expires: now.AddMinutes(30),
                signingCredentials: new SigningCredentials(settings.SigningKey, SecurityAlgorithms.HmacSha256));

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                token_type = "Bearer",
                expires_in = 1800
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
