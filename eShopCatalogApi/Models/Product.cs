using System.ComponentModel.DataAnnotations;

namespace eShopCatalogApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(64)]
        public string Sku { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
