using ApiAggregation.Models;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace ApiAggregation.Services
{
    /// <summary>
    /// Service for aggregating data from various APIs.
    /// Implements <see cref="IAggregationService"/>.
    /// </summary>
    public class AggregationService : IAggregationService
    {
        private readonly IApiService<WeatherRequest, WeatherResponse> _weatherService;
        private readonly IApiService<NewsRequest, NewsResponse> _newsService;
        private readonly IApiService<CountriesInfoRequest, CountriesInfoResponse> _countriesService;
        private readonly ICacheService _cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregationService"/> class.
        /// </summary>
        /// <param name="weatherService">Service for fetching weather data.</param>
        /// <param name="newsService">Service for fetching news data.</param>
        /// <param name="countriesService">Service for fetching countries information.</param>
        /// <param name="cacheService">Service for caching data.</param>
        public AggregationService(
            IApiService<WeatherRequest, WeatherResponse> weatherService,
            IApiService<NewsRequest, NewsResponse> newsService,
            IApiService<CountriesInfoRequest, CountriesInfoResponse> countriesService,
            ICacheService cacheService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _countriesService = countriesService;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Retrieves aggregated data from weather, news, and countries information services.
        /// </summary>
        /// <param name="weatherRequest">Request object for weather data.</param>
        /// <param name="newsRequest">Request object for news data.</param>
        /// <param name="countriesInfoRequest">Request object for countries information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the aggregated data.</returns>
        public async Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, CountriesInfoRequest countriesInfoRequest)
        {

            // Generate Redis cache keys based on request parameters to account for changes
            string weatherCacheKey = GenerateCacheKey("weather_data", weatherRequest);
            string newsCacheKey = GenerateCacheKey("news_data", newsRequest);
            string countriesInfoCacheKey = GenerateCacheKey("countries_data", countriesInfoRequest);

            // Use cache service to fetch data or call the API if not cached
            Task<WeatherResponse>? weatherTask = _cacheService.GetOrCreateAsync<WeatherResponse>(weatherCacheKey, () => _weatherService.GetDataAsync(weatherRequest));
            Task<NewsResponse>? newsTask = _cacheService.GetOrCreateAsync<NewsResponse>(newsCacheKey, () => _newsService.GetDataAsync(newsRequest));
            Task<CountriesInfoResponse>? countriesTask = _cacheService.GetOrCreateAsync<CountriesInfoResponse>(countriesInfoCacheKey, () => _countriesService.GetDataAsync(countriesInfoRequest));

            // Use parallelism to decrease response times
            await Task.WhenAll(weatherTask, newsTask, countriesTask);

            return new AggregatedData
            {
                Weather = await weatherTask,
                News = await newsTask,
                CountriesInfo = await countriesTask
            };
        }

        /// <summary>
        /// Generates a cache key based on the prefix and request parameters.
        /// </summary>
        /// <param name="prefix">The prefix for the cache key.</param>
        /// <param name="requestParams">The request parameters to be included in the cache key.</param>
        /// <returns>A string representing the generated cache key.</returns>
        private static string GenerateCacheKey(string prefix, object requestParams)
        {
            string serializedParams = JsonConvert.SerializeObject(requestParams);
            string paramHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedParams));
            return $"{prefix}_{paramHash}";
        }

    }
}
