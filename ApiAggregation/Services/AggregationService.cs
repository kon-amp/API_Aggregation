using ApiAggregation.Models;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services.Integrations;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace ApiAggregation.Services
{
    public class AggregationService
    {
        private readonly WeatherService _weatherService;
        private readonly NewsService _newsService;
        private readonly CountriesService _countriesService;
        private readonly ICacheService _cacheService;
        //private readonly SpotifyService _spotifyService;
        //private readonly GithubService _githubService;

        public AggregationService(WeatherService weatherService, NewsService newsService,CountriesService countriesService, ICacheService cacheService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _countriesService = countriesService;
            _cacheService = cacheService;
        }

        public async Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, CountriesInfoRequest countriesInfoRequest)
        {
           
            // Generate Redis Keys based on api request parameters to take account changes from the user
            string weatherCacheKey = GenerateCacheKey("weather_data", weatherRequest);
            string newsCacheKey = GenerateCacheKey("news_data", newsRequest);
            string countriesInfoCacheKey = GenerateCacheKey("countries_data", countriesInfoRequest);

            // Use cache service
            var weatherTask = _cacheService.GetOrCreateAsync<WeatherResponse>(weatherCacheKey, () => _weatherService.GetDataAsync(weatherRequest));
            var newsTask = _cacheService.GetOrCreateAsync<NewsResponse>(newsCacheKey, () => _newsService.GetDataAsync(newsRequest));
            var countriesTask = _cacheService.GetOrCreateAsync<CountriesInfoResponse>(countriesInfoCacheKey, () => _countriesService.GetDataAsync(countriesInfoRequest));
            
            //Task<SpotifyResponse> spotifyTask = _spotifyService.GetDataAsync(spotifyRequest);
            //Task<GithubResponse> githubTask = _githubService.GetDataAsync(githubRequest);

            // Using parallelism to decrease response times
            await Task.WhenAll(weatherTask, newsTask, countriesTask);

            return new AggregatedData
            {
                Weather = await weatherTask,
                News = await newsTask,
                CountriesInfo = await countriesTask
            };
        }

        private string GenerateCacheKey(string prefix, object requestParams)
        {
            string serializedParams = JsonConvert.SerializeObject(requestParams);
            string paramHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedParams));
            return $"{prefix}_{paramHash}";
        }

    }
}
