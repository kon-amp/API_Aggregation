using ApiAggregation.Models;

namespace ApiAggregation.Services
{
    public class NewsService : IApiService<NewsData>
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> CreateDataAsync(NewsData data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsData>> GetAllDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NewsData> GetDataAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<NewsData> GetDataByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDataAsync(string id, NewsData data)
        {
            throw new NotImplementedException();
        }
    }
}
