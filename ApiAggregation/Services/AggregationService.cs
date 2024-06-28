using ApiAggregation.Models;
using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;

namespace ApiAggregation.Services
{
    public class AggregationService
    {
        private readonly WeatherService _weatherService;
        private readonly NewsService _newsService;
        private readonly SpotifyService _spotifyService;
        private readonly GithubService _githubService;

        public AggregationService(WeatherService weatherService, NewsService newsService, SpotifyService spotifyService, GithubService githubService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
            _githubService = githubService;
        }

        public async Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, SpotifyArtistsRequest spotifyRequest, GithubRequest githubRequest)
        {
            Task<WeatherResponse> weatherTask = _weatherService.GetDataAsync(weatherRequest);
            Task<NewsResponse> newsTask = _newsService.GetDataAsync(newsRequest);
            Task<SpotifyResponse> spotifyTask = _spotifyService.GetDataAsync(spotifyRequest);
            Task<GithubResponse> githubTask = _githubService.GetDataAsync(githubRequest);

            await Task.WhenAll(weatherTask, newsTask, spotifyTask, githubTask);

            return new AggregatedData
            {
                Weather = await weatherTask,
                News = await newsTask,
                Spotify = await spotifyTask,
                Github = await githubTask
            };
        }
        
    }
}
