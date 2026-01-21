using Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entities
{
    public class Product : BaseEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid OperationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
