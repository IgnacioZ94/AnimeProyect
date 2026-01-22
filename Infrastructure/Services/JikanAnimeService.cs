using AutoMapper;
using Domain.Core.Configuration;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Infrastructure.Models.Jikan;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services;

public class JikanAnimeService : IAnimeService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ILogger<JikanAnimeService> _logger;
    private readonly JikanApiSettings _jikanSettings;

    public JikanAnimeService(
        HttpClient httpClient, 
        IMapper mapper, 
        ILogger<JikanAnimeService> logger,
        IOptions<JikanApiSettings> jikanSettings)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
        _jikanSettings = jikanSettings.Value;
    }

    public async Task<List<AnimeInfo>> GetTopAnimeAsync()
    {
        try
        {
            var url = $"{_jikanSettings.BaseUrl}{_jikanSettings.TopAnimeEndpoint}";
            _logger.LogInformation("Fetching top anime from Jikan API: {Url}", url);
            
            var response = await _httpClient.GetFromJsonAsync<JikanResponse>(url);
            
            if (response?.Data == null)
            {
                _logger.LogWarning("Jikan API returned null or empty data for top anime");
                return new List<AnimeInfo>();
            }

            var result = _mapper.Map<List<AnimeInfo>>(response.Data);
            _logger.LogInformation("Successfully fetched {Count} top anime from Jikan API", result.Count);
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error occurred while fetching top anime from Jikan API");
            return new List<AnimeInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching top anime from Jikan API");
            return new List<AnimeInfo>();
        }
    }

    public async Task<List<AnimeInfo>> SearchAnimeAsync(string query)
    {
        try
        {
            var url = $"{_jikanSettings.BaseUrl}{_jikanSettings.SearchAnimeEndpoint}?q={query}";
            _logger.LogInformation("Searching anime from Jikan API with query: {Query}, URL: {Url}", query, url);
            
            var response = await _httpClient.GetFromJsonAsync<JikanResponse>(url);
            
            if (response?.Data == null)
            {
                _logger.LogWarning("Jikan API returned null or empty data for search query: {Query}", query);
                return new List<AnimeInfo>();
            }

            var result = _mapper.Map<List<AnimeInfo>>(response.Data);
            _logger.LogInformation("Successfully fetched {Count} anime for query: {Query}", result.Count, query);
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error occurred while searching anime with query: {Query}", query);
            return new List<AnimeInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while searching anime with query: {Query}", query);
            return new List<AnimeInfo>();
        }
    }

    public async Task<List<AnimeInfo>> GetAnimeByCatalogAsync(string? query = null, bool? sfw = null)
    {
        try
        {
            var urlBuilder = $"{_jikanSettings.BaseUrl}{_jikanSettings.SearchAnimeEndpoint}?";
            
            if (!string.IsNullOrWhiteSpace(query))
            {
                urlBuilder += $"q={query}&";
            }
            
            if (sfw.HasValue)
            {
                urlBuilder += $"sfw={sfw.Value.ToString().ToLower()}&";
            }
            
            var url = urlBuilder.TrimEnd('&', '?');
            _logger.LogInformation("Fetching anime catalog from Jikan API - Query: {Query}, SFW: {Sfw}, URL: {Url}", 
                query ?? "none", sfw?.ToString() ?? "none", url);
            
            var response = await _httpClient.GetFromJsonAsync<JikanResponse>(url);
            
            if (response?.Data == null)
            {
                _logger.LogWarning("Jikan API returned null or empty data for catalog - Query: {Query}, SFW: {Sfw}", 
                    query ?? "none", sfw?.ToString() ?? "none");
                return new List<AnimeInfo>();
            }

            var result = _mapper.Map<List<AnimeInfo>>(response.Data);
            _logger.LogInformation("Successfully fetched {Count} anime for catalog - Query: {Query}, SFW: {Sfw}", 
                result.Count, query ?? "none", sfw?.ToString() ?? "none");
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error occurred while fetching anime catalog - Query: {Query}, SFW: {Sfw}", 
                query ?? "none", sfw?.ToString() ?? "none");
            return new List<AnimeInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching anime catalog - Query: {Query}, SFW: {Sfw}", 
                query ?? "none", sfw?.ToString() ?? "none");
            return new List<AnimeInfo>();
        }
    }
}
