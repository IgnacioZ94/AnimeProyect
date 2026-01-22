using Domain.Core.DTOs;

namespace Domain.Core.Interfaces;

public interface IAnimeService
{
    Task<List<AnimeInfo>> GetTopAnimeAsync();
    Task<List<AnimeInfo>> SearchAnimeAsync(string query);
}
