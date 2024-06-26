using ApiAggregation.Models;

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

        public async Task<AggregatedData> GetAggregatedDataAsync(string location, string topic, string hashtag)
        {
            var weatherTask = _weatherService.GetDataAsync(location);
            var newsTask = _newsService.GetDataAsync(topic);
            var spotifyTask = _spotifyService.GetDataAsync(hashtag);

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
