using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShopCatalogApi.Models
{
    public class ProductRequest : IValidatableObject
    {
        [Required, StringLength(64)]
        public string Sku { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Price.HasValue)
                yield break;

            if (Price.Value < 0m || Price.Value > 9999999999999999.99m)
            {
                yield return new ValidationResult(
                    "Price must be between 0 and 9999999999999999.99.",
                    new[] { nameof(Price) });
            }
            else if (decimal.Round(Price.Value, 2) != Price.Value)
            {
                yield return new ValidationResult(
                    "Price must not require rounding to two decimal places.",
                    new[] { nameof(Price) });
            }
        }
    }
}
