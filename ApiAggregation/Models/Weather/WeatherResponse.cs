using ApiAggregation.Models.News;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ApiAggregation.Models.Weather
{
    /// <summary>
    /// Represents the response from the weather API.
    /// </summary>
    public class WeatherResponse
    {
        [JsonProperty("coord")]
        public Coordinates Coord { get; set; } = new Coordinates();

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; } = new List<Weather>();

        /// <summary>
        /// Internal parameter
        /// </summary>
        [JsonProperty("base")]
        public string Base { get; set; } = string.Empty;

        [JsonProperty("main")]
        public Main Main { get; set; } = new Main();


        /// <summary>
        /// Visibility, meter. The maximum value of the visibility is 10 km
        /// </summary>
        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; } = new Wind();

        [JsonProperty("rain")]
        public Rain Rain { get; set; } = new Rain();

        [JsonProperty("snow")]
        public Snow Snow { get; set; } = new Snow();

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; } = new Clouds();

    public class CurrentWeather
    {

        /// <summary>
        /// Time of data calculation, unix, UTC
        /// </summary>
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("sys")]
        public SystemInfo Sys { get; set; } = new SystemInfo();

        /// <summary>
        /// Shift in seconds from UTC
        /// </summary>
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        /// <summary>
        /// City ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        ///  City name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Internal parameter
        /// </summary>
        [JsonProperty("cod")]
        public int Cod { get; set; }
    }

    public class Coordinates
    {
        /// <summary>
        /// Longitude of the location
        /// </summary>
        [JsonProperty("lon")]
        [Range(-180, 180, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude of the location
        /// </summary>
        [JsonProperty("lat")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double Latitude { get; set; }
    }

    public class Weather
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Group of weather parameters (Rain, Snow, Clouds etc.)
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        /// <summary>
        /// Weather condition within the group
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Weather icon id
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class Main
    {
        /// <summary>
        /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit
        /// </summary>
        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Temperature. This temperature parameter accounts for the human perception 
        /// of weather.
        /// </summary>
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        /// <summary>
        /// Minimum temperature at the moment. This is minimal currently observed temperature 
        /// (within large megalopolises and urban areas).
        /// </summary>
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        /// <summary>
        ///  Maximum temperature at the moment. This is maximal currently observed temperature 
        ///  (within large megalopolises and urban areas).
        /// </summary>
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        /// <summary>
        /// Humidity, %
        /// </summary>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }

        /// <summary>
        /// Atmospheric pressure on the ground level, hPa
        /// </summary>
        [JsonProperty("grnd_level")]
        public int GroundLevel { get; set; }
    }

    public class Wind
    {
        /// <summary>
        /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("deg")]
        public int Degree { get; set; }

        /// <summary>
        /// Wind gust. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
        /// </summary>
        [JsonProperty("gust")]
        public double Gust { get; set; }
    }

    public class Snow
    {
        /// <summary>
        ///  Snow volume for the last 1 hour, mm. Please note that only mm as 
        ///  units of measurement are available for this parameter
        /// </summary>
        [JsonProperty("1h")]
        public double? OneHour { get; set; }

        /// <summary>
        /// Snow volume for the last 3 hours, mm. Please note that only mm as 
        /// units of measurement are available for this parameter
        /// </summary>
        [JsonProperty("3h")]
        public double? ThreeHour { get; set; }
    }

    public class Rain
    {
        /// <summary>
        /// Rain volume for the last 1 hour, mm. Please note that only mm as 
        /// units of measurement are available for this parameter
        /// </summary>
        [JsonProperty("1h")]
        public double? OneHour { get; set; }

        /// <summary>
        /// Rain volume for the last 3 hours, mm. Please note that only mm as 
        /// units of measurement are available for this parameter
        /// </summary>
        [JsonProperty("3h")]
        public double? ThreeHour { get; set; }
    }

    public class Clouds
    {
        /// <summary>
        /// Cloudiness, %
        /// </summary>
        [JsonProperty("all")]
        public int All { get; set; }
    }

    public class SystemInfo
    {
        /// <summary>
        /// Internal parameter
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// Internal parameter
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Internal parameter
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Country code (GB, JP etc.)
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Sunrise time, unix, UTC
        /// </summary>
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        /// <summary>
        /// Sunset time, unix, UTC
        /// </summary>
        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

}
