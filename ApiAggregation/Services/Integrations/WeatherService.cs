using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiAggregation.Services.Integrations
{
    /// <summary>
    /// Service for interacting with the weather API.
    /// Implements <see cref="IApiService{WeatherRequest, WeatherResponse}"/>.
    /// </summary>
    public class WeatherService : IApiService<WeatherRequest, WeatherResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiSettings _settings;
        private readonly ILogger<WeatherService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for API requests.</param>
        /// <param name="settings">Settings for the weather API.</param>
        /// <param name="logger">Logger for logging messages and errors.</param>
        public WeatherService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value.Weather;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves weather information based on the provided request.
        /// </summary>
        /// <param name="request">Request containing parameters for fetching weather data.</param>
        /// <returns>A <see cref="WeatherResponse"/> containing the weather data.</returns>
        public async Task<WeatherResponse> GetDataAsync(WeatherRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request object is null.");
                return new WeatherResponse();
            }

            if (string.IsNullOrEmpty(_settings.ApiKey) && string.IsNullOrEmpty(request.ApiKey))
            {
                _logger.LogError("ApiKey is required");
                return new WeatherResponse();
            }

            string? apikey = string.IsNullOrEmpty(request.ApiKey) ? _settings.ApiKey : request.ApiKey;
            string? url = $"{_settings.BaseUrl}?lat={request.Latitude}&lon={request.Longitude}&appid={apikey}";

            HttpResponseMessage? response;
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation");
                response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string? content = await response.Content.ReadAsStringAsync();

                WeatherResponse? weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

                if (weatherResponse == null)
                {
                    _logger.LogError("Failed to deserialize the response.");
                    return new WeatherResponse();
                }

                return weatherResponse;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"HTTP Request Error: {e.Message}, Status Code: {e.StatusCode}");
                return new WeatherResponse();
            }
            catch (Exception e)
            {
                _logger.LogError($"An unexpected error occurred: {e.Message}");
                return new WeatherResponse();
            }

        }

    }
}
