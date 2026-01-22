namespace Domain.Core.DTOs;

public class AnimeInfo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
    public double? Score { get; set; }
}
