using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Models.Jikan
{
    public class JikanResponse
    {
        [JsonPropertyName("data")]
        public List<JikanAnimeItem>? Data { get; set; }
    }
}
