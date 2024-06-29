using ApiAggregation.Models.Github;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;

namespace ApiAggregation.Services.Integrations
{
    public class GithubService : IApiService<GithubRequest, GithubResponse>
    {
        private readonly HttpClient _httpClient;

        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GithubResponse> GetDataAsync(GithubRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.ApiKey))
            {
                throw new ArgumentException("Username and ApiKey must be provided.");
            }

            // TO DO - to be changed and parsed from appsettings - then create it 
            string url = $"https://api.github.com/users/{request.Username}/repos?type={request.Type}&sort={request.Sort}&direction={request.Direction}&per_page={request.PerPage}&page={request.Page}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.ApiKey}");
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation");
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            GithubResponse? githubResponse = JsonConvert.DeserializeObject<GithubResponse>(content);

            if (githubResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return githubResponse;
        }
    }
}