﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Entities;

namespace CartsService.Entities
{
    public class Product : Entity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }
    }
}
