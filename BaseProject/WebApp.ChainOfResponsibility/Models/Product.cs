using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.ChainOfResponsibility.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
