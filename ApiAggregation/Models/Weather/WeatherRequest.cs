using Swashbuckle.AspNetCore.Annotations;

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
        public double Latitude { get; set; } = 37.97;

        /// <summary>
        /// The longitude of the location.
        /// </summary>
        public double Longitude { get; set; } = 23.73;

        /// <summary>
        /// The Weather API key for authenticating the request.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
