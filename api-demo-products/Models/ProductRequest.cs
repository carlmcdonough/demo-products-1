using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_demo_products.Models
{
    public class ProductRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}