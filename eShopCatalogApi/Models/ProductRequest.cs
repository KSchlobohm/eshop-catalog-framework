using System.ComponentModel.DataAnnotations;

namespace eShopCatalogApi.Models
{
    public class ProductRequest
    {
        [Required, StringLength(64)]
        public string Sku { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }

        [Required, Range(typeof(decimal), "0", "99999999.99")]
        public decimal? Price { get; set; }
    }
}
