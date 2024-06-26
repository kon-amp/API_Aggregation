using ApiAggregation.Models;
using System.Text.Json;

namespace ApiAggregation.Services
{
    public class WeatherService : IApiService<WeatherData>
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> CreateDataAsync(WeatherData data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WeatherData>> GetAllDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WeatherData> GetDataAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherData> GetDataByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDataAsync(string id, WeatherData data)
        {
            throw new NotImplementedException();
        }
    }
}
