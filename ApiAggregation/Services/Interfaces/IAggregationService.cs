using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Weather;
using ApiAggregation.Models;

namespace ApiAggregation.Services.Interfaces
{
    public interface IAggregationService
    {
        Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, CountriesInfoRequest countriesInfoRequest);
    }
}
