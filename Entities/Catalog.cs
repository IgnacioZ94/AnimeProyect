using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entities
{
    public class Catalog : BaseEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid OperationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
