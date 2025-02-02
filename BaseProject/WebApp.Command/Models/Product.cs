using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Command.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
