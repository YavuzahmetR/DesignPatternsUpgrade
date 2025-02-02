using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Strategy.Models
{
    public sealed class Product
    {
        public Product()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]    
        public string? Id { get; set; } = null!;

        public string Name { get; set; } = null!;


        [BsonRepresentation(BsonType.Decimal128)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string? UserId { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
