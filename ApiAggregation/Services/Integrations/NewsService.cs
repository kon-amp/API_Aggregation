using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.Enums;
using ApiAggregation.Models.News;
using ApiAggregation.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiAggregation.Services.Integrations
{
    public class NewsService : IApiService<NewsRequest, NewsResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly NewsApiSettings _settings;
        private readonly ILogger<NewsService> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="NewsService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for API requests.</param>
        /// <param name="settings">Settings for the news API.</param>
        /// <param name="logger">Logger for logging messages and errors.</param>
        public NewsService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<NewsService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value.NewsApi;
            _logger = logger;
        }


        /// <summary>
        /// Retrieves news articles based on the provided request.
        /// </summary>
        /// <param name="request">Request containing parameters for fetching news.</param>
        /// <returns>A <see cref="NewsResponse"/> containing the breaking news articles.</returns>
        public async Task<NewsResponse> GetDataAsync(NewsRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request object is null.");
                return new NewsResponse { Status = "Error", TotalResults = 0, Articles = new List<Article>() };
            }

            if (string.IsNullOrEmpty(_settings.ApiKey) && string.IsNullOrEmpty(request.ApiKey))
            {
                _logger.LogError("ApiKey is required but not provided.");
                return new NewsResponse { Status = "ApiKey is Required", TotalResults = 0, Articles = new List<Article>() };
            }

            string? apikey = string.IsNullOrEmpty(request.ApiKey) ? _settings.ApiKey : request.ApiKey;
            string countryOption = request.Country.GetEnumMemberValue();
            string? url = $"{_settings.BaseUrl}?country={countryOption}&apiKey={apikey}";

            HttpResponseMessage? response;
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation");
                response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string? content = await response.Content.ReadAsStringAsync();
                NewsResponse? newsResponse = JsonConvert.DeserializeObject<NewsResponse>(content);

                if (newsResponse == null)
                {

                    _logger.LogError("Failed to deserialize the response.");
                    return new NewsResponse { Status = "Failed to deserialize the response.", TotalResults = 0, Articles = new List<Article>() };
                }

                return newsResponse;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("HTTP Request Error: {Message}, Status Code: {StatusCode}", e.Message, e.StatusCode);
                return GetFallbackData(e);
            }
            catch (Exception e)
            {
                _logger.LogError("An unexpected error occurred: {Message}", e.Message);
                return new NewsResponse { Status = "Error", TotalResults = 0, Articles = new List<Article>() };
            }
        }

        /// <summary>
        /// Returns fallback data in case of an HTTP request error.
        /// </summary>
        /// <param name="e">The exception that occurred during the HTTP request.</param>
        /// <returns>A <see cref="NewsResponse"/> with fallback article.</returns>
        private NewsResponse GetFallbackData(HttpRequestException e)
        {
            _logger.LogInformation("Returning fallback data.");
            var fallbackArticles = new List<Article> { new Article {
            Source = new Source
            {
                Id = "fallback-source",
                Name = "Fallback News"
            },
            Author = "Fallback Author",
            Title = "Fallback Title: News Unavailable",
            Description = e.Message,
            Url = string.Empty,
            UrlToImage = string.Empty,
            PublishedAt = DateTime.UtcNow,
            Content = "Due to an issue with our news provider, this fallback content is being displayed. We apologize for any inconvenience."
                }
            };

            return new NewsResponse
            {
                Status = e.StatusCode?.ToString() ?? "500",
                TotalResults = fallbackArticles.Count,
                Articles = fallbackArticles
            };
        }
    }
}
