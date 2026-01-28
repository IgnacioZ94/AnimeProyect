using System.Text.Json.Serialization;

namespace Infrastructure.Models.Jikan
{
    public class JikanTrailer
    {
        [JsonPropertyName("youtube_id")]
        public string? YoutubeId { get; set; }
        
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        
        [JsonPropertyName("embed_url")]
        public string? EmbedUrl { get; set; }
    }
}
