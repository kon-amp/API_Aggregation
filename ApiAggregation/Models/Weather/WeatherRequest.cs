namespace ApiAggregation.Models.Weather
{
    /// <summary>
    /// Represents a request to get weather information from the OpenWeatherMap API.
    /// </summary>
    public class WeatherRequest
    {
        /// <summary>
        /// The latitude of the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of the location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The API key for authenticating the request.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
