using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Services;

public class JikanAnimeService : IAnimeService
{
    private readonly HttpClient _httpClient;

    public JikanAnimeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AnimeInfo>> GetTopAnimeAsync()
    {
        try
        {
             var response = await _httpClient.GetFromJsonAsync<JikanResponse>("https://api.jikan.moe/v4/top/anime");
             return response?.Data?.Select(MapToEntity).ToList() ?? new List<AnimeInfo>();
        }
        catch (Exception ex)
        {
            // Log exception or handle it. For now returning empty list or rethrow.
            // Ideally we should log this.
            return new List<AnimeInfo>(); 
        }
    }

    public async Task<List<AnimeInfo>> SearchAnimeAsync(string query)
    {
        try
        {
             var response = await _httpClient.GetFromJsonAsync<JikanResponse>($"https://api.jikan.moe/v4/anime?q={query}");
             return response?.Data?.Select(MapToEntity).ToList() ?? new List<AnimeInfo>();
        }
        catch(Exception ex)
        {
             return new List<AnimeInfo>(); 
        }
    }

    private AnimeInfo MapToEntity(JikanAnimeItem item)
    {
        return new AnimeInfo
        {
            Id = item.MalId,
            Title = item.Title,
            Synopsis = item.Synopsis,
            ImageUrl = item.Images?.Jpg?.ImageUrl,
            Score = item.Score
        };
    }

    // Helper classes for Jikan API response
    private class JikanResponse
    {
        [JsonPropertyName("data")]
        public List<JikanAnimeItem>? Data { get; set; }
    }

    private class JikanAnimeItem
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

    private class JikanImages
    {
        [JsonPropertyName("jpg")]
        public JikanImageUrls? Jpg { get; set; }
    }

    private class JikanImageUrls
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
