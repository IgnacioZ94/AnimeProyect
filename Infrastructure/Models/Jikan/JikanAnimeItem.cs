using System.Text.Json.Serialization;

namespace Infrastructure.Models.Jikan
{
    public class JikanAnimeItem
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("synopsis")]
        public string? Synopsis { get; set; }
        
        [JsonPropertyName("score")]
        public double? Score { get; set; }
        
        [JsonPropertyName("images")]
        public JikanImages? Images { get; set; }
    }
}
