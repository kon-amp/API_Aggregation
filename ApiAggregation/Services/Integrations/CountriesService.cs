using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Web;

namespace ApiAggregation.Services.Integrations
{
    /// <summary>
    /// Service for interacting with the countries API.
    /// Implements <see cref="IApiService{CountriesInfoRequest, CountriesInfoResponse}"/>.
    /// </summary>
    public class CountriesService : IApiService<CountriesInfoRequest, CountriesInfoResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly CountriesApiSettings _settings;
        private readonly ILogger<CountriesService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <param name="settings">Settings for the countries API.</param>
        /// <param name="logger">Logger for logging messages.</param>
        public CountriesService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<CountriesService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value.CountriesApi;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves country information based on the provided request.
        /// </summary>
        /// <param name="request">Request containing country name for filtering.</param>
        /// <returns>A <see cref="CountriesInfoResponse"/> containing country data.</returns>
        public async Task<CountriesInfoResponse> GetDataAsync(CountriesInfoRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request object is null.");
                return new CountriesInfoResponse();
            }

            string apiCallOption = string.IsNullOrEmpty(request.CountryName) ? "/all" : $"/name/{HttpUtility.UrlEncode(request.CountryName)}";
            string url = $"{_settings.BaseUrl}{apiCallOption}?fields={_settings.FieldsForFiltering}";

            HttpResponseMessage? response;
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation");
                response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string? content = await response.Content.ReadAsStringAsync();

                // API response can return many objects of CountriesInfo class
                List<CountriesInfo> countriesInfo = JsonConvert.DeserializeObject<List<CountriesInfo>>(content) ?? new List<CountriesInfo>();
                CountriesInfoResponse countriesResponse = new() { CountriesInfo = countriesInfo };
                if (countriesResponse == null)
                {

                    _logger.LogError("Failed to deserialize the response.");
                    return new CountriesInfoResponse();
                }

                return countriesResponse;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("HTTP Request Error: {Message}, Status Code: {StatusCode}", e.Message, e.StatusCode);
                return new CountriesInfoResponse();
            }
            catch (Exception e)
            {
                _logger.LogError("An unexpected error occurred: {Message}", e.Message);
                return new CountriesInfoResponse();
            }
        }
    }
}
