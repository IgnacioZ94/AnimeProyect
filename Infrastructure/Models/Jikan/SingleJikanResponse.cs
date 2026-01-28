using System.Text.Json.Serialization;

namespace Infrastructure.Models.Jikan
{
    public class SingleJikanResponse
    {
        [JsonPropertyName("data")]
        public JikanAnimeItem? Data { get; set; }
    }
}
