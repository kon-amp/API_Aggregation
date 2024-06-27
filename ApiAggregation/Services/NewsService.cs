using ApiAggregation.Models.News;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;

namespace ApiAggregation.Services
{
    public class NewsService : IApiService<NewsRequest, NewsResponse>
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NewsResponse> GetDataAsync(NewsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.Country) || string.IsNullOrEmpty(request.ApiKey))
            {
                throw new ArgumentException("Country and ApiKey must be provided.");
            }

            // TO DO - to be changed and parsed from appsettings - then create it 
            string? url = $"https://newsapi.org/v2/top-headlines?country={request.Country}&apiKey={request.ApiKey}";
            HttpResponseMessage? response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            NewsResponse? newsResponse = JsonConvert.DeserializeObject<NewsResponse>(content);

            if (newsResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return newsResponse;
        }
    }
}
