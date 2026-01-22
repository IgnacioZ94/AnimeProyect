using System;

namespace Domain.Core.DTOs
{
    public class CatalogRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid OperationId { get; set; }
    }
}
