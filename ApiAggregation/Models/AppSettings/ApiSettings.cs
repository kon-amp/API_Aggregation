using ApiAggregation.Models.User;
using StackExchange.Redis;

namespace ApiAggregation.Models.AppSettings
{
    /// <summary>
    /// Represents the API settings configured in appsettings.json.
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// Gets or sets the settings for the login of the user
        /// </summary>
        public LoginModelSettings LoginModel { get; set; } = new LoginModelSettings();
        public WeatherApiSettings Weather { get; set; } = new WeatherApiSettings();
        public NewsApiSettings NewsApi { get; set; } = new NewsApiSettings();
        public CountriesApiSettings CountriesApi { get; set; } = new CountriesApiSettings();
        public SpotifyApiSettings SpotifyApi { get; set; } = new SpotifyApiSettings();
        public GitHubApiSettings GitHubApi { get; set; } = new GitHubApiSettings();

    }

    public class GitHubApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string AcceptHeader { get; set; } = string.Empty;
        public string AuthorizationHeader { get; set; } = string.Empty;
        public string? ApiVersion { get; set; }
        public string DefaultType { get; set; } = "owner";
        public string DefaultSort { get; set; } = "full_name";
        public string DefaultDirection { get; set; } = "asc";
        public int DefaultPerPage { get; set; } = 30;
    }

    public class NewsApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string DefaultCountry { get; set; } = "gr";
    }

    public class SpotifyApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string AuthorizationHeader { get; set; } = string.Empty;
    }

    public class WeatherApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }

    public class CountriesApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string FieldsForFiltering { get; set; } = string.Empty;
    }

    public class LoginModelSettings
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
