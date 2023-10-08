using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_demo_products.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}