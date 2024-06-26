using ApiAggregation.Models;

namespace ApiAggregation.Services
{
    public class SpotifyService : IApiService<SpotifyData>
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> CreateDataAsync(SpotifyData data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SpotifyData>> GetAllDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SpotifyData> GetDataAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<SpotifyData> GetDataByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDataAsync(string id, SpotifyData data)
        {
            throw new NotImplementedException();
        }
    }
}
