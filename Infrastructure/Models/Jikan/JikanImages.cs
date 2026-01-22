using System.Text.Json.Serialization;

namespace Infrastructure.Models.Jikan
{
    public class JikanImages
    {
        [JsonPropertyName("jpg")]
        public JikanImageUrls? Jpg { get; set; }
    }

    public class JikanImageUrls
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
