using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Decorator.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? UserId { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
