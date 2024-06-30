using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;

namespace ApiAggregation.Models
{
    public class AggregatedData
    {
        public WeatherResponse Weather { get; set; } = new WeatherResponse();
        public NewsResponse News { get; set; } = new NewsResponse();
        public CountriesInfoResponse CountriesInfo { get; set; } = new CountriesInfoResponse();

        //public SpotifyResponse Spotify { get; set; } = new SpotifyResponse();

        //public GithubResponse Github { get; set; } = new GithubResponse();
    }
}
