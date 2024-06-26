namespace ApiAggregation.Models
{
    public class AggregatedData
    {
        public WeatherData Weather { get; set; }
        public NewsData News { get; set; }
        public SpotifyData Spotify { get; set; }
    }
}
