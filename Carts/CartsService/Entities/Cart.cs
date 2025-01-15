using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Entities;

namespace CartsService.Entities
{
    public class Cart : Entity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
