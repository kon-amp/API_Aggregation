using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Weather;
using ApiAggregation.Models;

namespace ApiAggregation.Services.Interfaces
{
    /// <summary>
    /// Interface for services that aggregate data from multiple sources.
    /// </summary>
    public interface IAggregationService
    {
        /// <summary>
        /// Retrieves aggregated data based on the provided requests.
        /// </summary>
        /// <param name="weatherRequest">Request for weather data.</param>
        /// <param name="newsRequest">Request for news data.</param>
        /// <param name="countriesInfoRequest">Request for countries information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the aggregated data.</returns>
        Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, CountriesInfoRequest countriesInfoRequest);
    }
}
