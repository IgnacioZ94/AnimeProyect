namespace Domain.Core.Configuration
{
    public class JikanApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string TopAnimeEndpoint { get; set; } = string.Empty;
        public string SearchAnimeEndpoint { get; set; } = string.Empty;
    }
}
