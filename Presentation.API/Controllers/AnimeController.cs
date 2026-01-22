using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IAnimeService _animeService;

    public AnimeController(IAnimeService animeService)
    {
        _animeService = animeService;
    }

    [HttpGet("top")]
    public async Task<ActionResult<List<AnimeInfo>>> GetTopAnime()
    {
        var result = await _animeService.GetTopAnimeAsync();
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<AnimeInfo>>> SearchAnime([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query parameter is required.");
        }
        var result = await _animeService.SearchAnimeAsync(query);
        return Ok(result);
    }

    [HttpGet("catalog")]
    public async Task<ActionResult<List<AnimeInfo>>> GetAnimeCatalog(
        [FromQuery] string? q = null, 
        [FromQuery] bool? sfw = null)
    {
        var result = await _animeService.GetAnimeByCatalogAsync(q, sfw);
        return Ok(result);
    }
}
