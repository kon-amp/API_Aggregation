using ApiAggregation.Models;
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

        public AggregationService(WeatherService weatherService, NewsService newsService, SpotifyService spotifyService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
        }

        public async Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, SpotifyArtistsRequest spotifyRequest)
        {
            Task<WeatherResponse> weatherTask = _weatherService.GetDataAsync(weatherRequest);
            Task<NewsResponse> newsTask = _newsService.GetDataAsync(newsRequest);
            Task<SpotifyResponse> spotifyTask = _spotifyService.GetDataAsync(spotifyRequest);

            await Task.WhenAll(weatherTask, newsTask, spotifyTask);

            return new AggregatedData
            {
                Weather = await weatherTask,
                News = await newsTask,
                Spotify = await spotifyTask
            };
        }
        
    }
}
