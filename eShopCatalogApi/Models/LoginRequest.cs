using System.ComponentModel.DataAnnotations;

namespace eShopCatalogApi.Models
{
    public class LoginRequest
    {
        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
