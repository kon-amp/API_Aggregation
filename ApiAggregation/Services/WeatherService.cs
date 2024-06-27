using ApiAggregation.Models.Weather;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;

namespace ApiAggregation.Services
{
    public class WeatherService : IApiService<WeatherRequest, WeatherResponse>
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetDataAsync(WeatherRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.ApiKey))
            {
                throw new ArgumentException("ApiKey is required");
            }

            // TO DO - to be changed and parsed from appsettings - then create it 
            var url = $"https://api.openweathermap.org/data/3.0/onecall?lat={request.Latitude}&lon={request.Longitude}&appid={request.ApiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

            if (weatherResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return weatherResponse;
        }

    }
}
