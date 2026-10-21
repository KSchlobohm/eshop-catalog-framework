using System.ComponentModel.DataAnnotations;

namespace eShopCatalogApi.Models
{
    public class ApiUser
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
