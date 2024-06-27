using ApiAggregation.Models.Spotify;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;

namespace ApiAggregation.Services
{
    public class SpotifyService : IApiService<SpotifyArtistsRequest, SpotifyResponse>
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SpotifyResponse> GetDataAsync(SpotifyArtistsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(request.Ids))
            {
                throw new ArgumentException("Request needs at least one spotify artist id");
            }

            HttpResponseMessage? response = await _httpClient.GetAsync($"/artists?ids={request.Ids}");
            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync();
            SpotifyResponse? spotifyResponse = JsonConvert.DeserializeObject<SpotifyResponse>(content);

            if (spotifyResponse == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response.");
            }

            return spotifyResponse;
        }

    }
}
