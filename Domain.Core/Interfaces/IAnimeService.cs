using Domain.Core.DTOs;

namespace Domain.Core.Interfaces;

public interface IAnimeService
{
    Task<List<AnimeInfo>> GetTopAnimeAsync();
    Task<List<AnimeInfo>> SearchAnimeAsync(string query);
    Task<List<AnimeInfo>> GetAnimeByCatalogAsync(string? query = null, bool? sfw = null);
}
