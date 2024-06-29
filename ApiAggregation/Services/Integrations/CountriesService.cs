using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Web;

namespace ApiAggregation.Services.Integrations
{
    public class CountriesService : IApiService<CountriesInfoRequest, CountriesInfoResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly CountriesApiSettings _settings;
        private readonly ILogger<CountriesService> _logger;

        public CountriesService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<CountriesService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value.CountriesApi;
            _logger = logger;
        }

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
                CountriesInfoResponse countriesResponse = new CountriesInfoResponse { CountriesInfo = countriesInfo };
                if (countriesResponse == null)
                {

                    _logger.LogError("Failed to deserialize the response.");
                    return new CountriesInfoResponse();
                }

                return countriesResponse;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"HTTP Request Error: {e.Message}, Status Code: {e.StatusCode}");
                return new CountriesInfoResponse();
            }
            catch (Exception e)
            {
                _logger.LogError($"An unexpected error occurred: {e.Message}");
                return new CountriesInfoResponse();
            }
        }
    }
}
