using System;

namespace Domain.Core.DTOs
{
    public class CatalogResponse
    {
        public Guid OperationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int AnimeId { get; set; }
    }
}
