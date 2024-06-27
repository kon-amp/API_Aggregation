using ApiAggregation.Models.News;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models.Weather
{
    /// <summary>
    /// Represents the response from the weather API.
    /// </summary>
    public class WeatherResponse
    {
        /// <summary>
        /// Latitude of the location
        /// </summary>
        [JsonProperty("lat")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double Lat { get; set; }

        /// <summary>
        /// Longitude of the location
        /// </summary>
        [JsonProperty("lon")]
        [Range(-180, 180, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double Lon { get; set; }

        /// <summary>
        /// Timezone name for the requested location
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; } = string.Empty;

        /// <summary>
        /// Shift in seconds from UTC
        /// </summary>
        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }


        /// <summary>
        /// Current weather data API response
        /// </summary>
        [JsonProperty("current")]
        public CurrentWeather Current { get; set; } = new CurrentWeather();

        /// <summary>
        /// Minute forecast weather data API response
        /// </summary>
        [JsonProperty("minutely")]
        public List<MinutelyWeather> Minutely { get; set; } = new List<MinutelyWeather>();

        /// <summary>
        /// Hourly forecast weather data API response
        /// </summary>
        [JsonProperty("hourly")]
        public List<HourlyWeather> Hourly { get; set; } = new List<HourlyWeather>();

        /// <summary>
        /// Daily forecast weather data API response
        /// </summary>
        [JsonProperty("daily")]
        public List<DailyWeather> Daily { get; set; } = new List<DailyWeather>();

        /// <summary>
        /// National weather alerts data from major national weather warning systems
        /// </summary>
        [JsonProperty("alerts")]
        public List<WeatherAlert> Alerts { get; set; } = new List<WeatherAlert>();
    }

    public class CurrentWeather
    {

        /// <summary>
        /// Current time, Unix, UTC
        /// </summary>
        [JsonProperty("dt")]
        public int Dt { get; set; }

        /// <summary>
        /// Sunrise time, Unix, UTC
        /// </summary>
        [JsonProperty("sunrise")]
        public int? Sunrise { get; set; }

        /// <summary>
        /// Sunset time, Unix, UTC
        /// </summary>
        [JsonProperty("sunset")]
        public int? Sunset { get; set; }

        /// <summary>
        /// Temperature. 
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
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("presure")]
        public int Pressure { get; set; }

        /// <summary>
        /// Humidity ( % )
        /// </summary>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Atmospheric temperature (varying according to pressure and humidity) below 
        /// which water droplets begin to condense and dew can form.
        /// </summary>
        [JsonProperty("drew_point")]
        public double DewPoint { get; set; }

        /// <summary>
        /// Current UV index.
        /// </summary>
        [JsonProperty("uvi")]
        public double Uvi { get; set; }

        /// <summary>
        /// Cloudiness ( % )
        /// </summary>
        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        /// <summary>
        ///  Average visibility, metres.
        /// </summary>
        [JsonProperty("visibility")]
        [Range(0, 10000, ErrorMessage = "Visibility cannot exceed 10km.")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Wind direction, degrees
        /// </summary>
        [JsonProperty("win_deg")]
        public int WindDeg { get; set; }

        /// <summary>
        /// Wind gust. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.
        /// </summary>
        [JsonProperty("win_gust")]
        public double? WindGust { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDescription> Weather { get; set; } = new List<WeatherDescription>();
    }

    public class MinutelyWeather
    {
        /// <summary>
        /// Time of the forecasted data, unix, UTC
        /// </summary>
        [JsonProperty("dt")]
        public int Dt { get; set; }

        /// <summary>
        /// Precipitation, mm/h. Please note that only mm/h as 
        /// units of measurement are available for this parameter
        /// </summary>
        [JsonProperty("precipation")]
        public double Precipitation { get; set; }
    }

    public class HourlyWeather
    {
        /// <summary>
        /// Time of the forecasted data, Unix, UTC
        /// </summary>
        [JsonProperty("dt")]
        public int Dt { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Temperature. This accounts for the human perception of weather. 
        /// Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        /// <summary>
        ///  Humidity ( % )
        /// </summary>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Atmospheric temperature (varying according to pressure and humidity) below 
        /// which water droplets begin to condense and dew can form. 
        /// Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        /// <summary>
        /// UV index
        /// </summary>
        [JsonProperty("uvi")]
        public double Uvi { get; set; }

        /// <summary>
        ///  Cloudiness ( % )
        /// </summary>
        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        /// <summary>
        /// Average visibility, metres.
        /// </summary>
        [JsonProperty("visibility")]
        [Range(0, 10000, ErrorMessage = "Visibility cannot exceed 10km.")]
        public int Visibility { get; set; }

        /// <summary>
        /// Wind speed. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.
        /// </summary>
        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("wind_deg")]
        public int WindDeg { get; set; }

        /// <summary>
        /// Wind gust. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.
        /// </summary>
        [JsonProperty("wind_gust")]
        public double? WindGust { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDescription> Weather { get; set; } = new List<WeatherDescription>();

        /// <summary>
        /// Probability of precipitation. The values of the parameter vary between 0 and 1, 
        /// where 0 is equal to 0%, 1 is equal to 100%
        /// </summary>
        [JsonProperty("pop")]
        public double Pop { get; set; }
    }

    public class DailyWeather
    {
        /// <summary>
        /// Time of the forecasted data, Unix, UTC
        /// </summary>
        [JsonProperty("dt")]
        public int Dt { get; set; }

        /// <summary>
        /// Sunrise time, Unix, UTC
        /// </summary>
        [JsonProperty("sunrise")]
        public int? Sunrise { get; set; }

        /// <summary>
        /// Sunset time, Unix, UTC.
        /// </summary>
        [JsonProperty("sunset")]
        public int? Sunset { get; set; }

        /// <summary>
        /// The time of when the moon rises for this day, Unix, UTC
        /// </summary>
        [JsonProperty("moonrise")]
        public int Moonrise { get; set; }

        /// <summary>
        /// The time of when the moon sets for this day, Unix, UTC
        /// </summary>
        [JsonProperty("moonset")]
        public int Moonset { get; set; }

        /// <summary>
        /// Moon phase. 0 and 1 are 'new moon', 0.25 is 'first quarter moon', 0.5 is 'full moon' 
        /// and 0.75 is 'last quarter moon'. The periods in between are called 'waxing crescent', 
        /// 'waxing gibbous', 'waning gibbous', and 'waning crescent', respectively. Moon phase 
        /// calculation algorithm: if the moon phase values between the start of the day and the 
        /// end of the day have a round value (0, 0.25, 0.5, 0.75, 1.0), then this round value is 
        /// taken, otherwise the average of moon phases for the start of the day and the end of the 
        /// day is taken
        /// </summary>
        [JsonProperty("moon_phase")]
        public double MoonPhase { get; set; }

        /// <summary>
        /// Human-readable description of the weather conditions for the day
        /// </summary>
        [JsonProperty("summmary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.
        /// </summary>
        public Temperature Temp { get; set; } = new Temperature();

        /// <summary>
        /// This accounts for the human perception of weather. 
        /// Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.
        /// </summary>
        public FeelsLike FeelsLike { get; set; } = new FeelsLike();

        /// <summary>
        /// Atmospheric pressure on the sea level, hPa
        /// </summary>
        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        /// <summary>
        /// Humidity ( % )
        /// </summary>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Atmospheric temperature (varying according to pressure and humidity) 
        /// below which water droplets begin to condense and dew can form. 
        /// Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.
        /// </summary>
        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        /// <summary>
        /// Wind speed. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.
        /// </summary>
        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("wind_deg")]
        public int WindDeg { get; set; }

        /// <summary>
        /// Wind gust. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.
        /// </summary>
        [JsonProperty("wind_gust")]
        public double? WindGust { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDescription> Weather { get; set; } = new List<WeatherDescription>();

        /// <summary>
        /// Cloudiness ( % )
        /// </summary>
        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        /// <summary>
        /// Probability of precipitation. The values of the parameter vary 
        /// between 0 and 1, where 0 is equal to 0%, 1 is equal to 100%
        /// </summary>
        [JsonProperty("pop")]
        public double Pop { get; set; }

        /// <summary>
        /// Precipitation volume, mm
        /// </summary>
        [JsonProperty("rain")]
        public double? Rain { get; set; }

        /// <summary>
        /// The maximum value of UV index for the day
        /// </summary>
        [JsonProperty("uvi")]
        public double Uvi { get; set; }
    }

    public class Temperature
    {
        /// <summary>
        /// Day temperature.
        /// </summary>
        [JsonProperty("day")]
        public double Day { get; set; }

        /// <summary>
        /// Min daily temperature.
        /// </summary>
        [JsonProperty("min")]
        public double Min { get; set; }

        /// <summary>
        /// Max daily temperature.
        /// </summary>
        [JsonProperty("max")]
        public double Max { get; set; }

        /// <summary>
        /// Night temperature.
        /// </summary>
        [JsonProperty("night")]
        public double Night { get; set; }

        /// <summary>
        /// Evening temperature.
        /// </summary>
        [JsonProperty("eve")]
        public double Eve { get; set; }

        /// <summary>
        /// Morning temperature.
        /// </summary>
        [JsonProperty("morn")]
        public double Morn { get; set; }
    }

    public class FeelsLike
    {
        /// <summary>
        /// Day temperature.
        /// </summary>
        [JsonProperty("day")]
        public double Day { get; set; }

        /// <summary>
        /// Night temperature.
        /// </summary>
        [JsonProperty("night")]
        public double Night { get; set; }

        /// <summary>
        /// Evening temperature.
        /// </summary>
        [JsonProperty("eve")]
        public double Eve { get; set; }

        /// <summary>
        /// Morning temperature.
        /// </summary>
        [JsonProperty("morn")]
        public double Morn { get; set; }
    }

    public class WeatherDescription
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Group of weather parameters
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; }

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

    public class WeatherAlert
    {
        /// <summary>
        /// Name of the alert source
        /// </summary>
        [JsonProperty("sender_name")]
        public string SenderName { get; set; } = string.Empty;

        /// <summary>
        /// Alert event name
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of the start of the alert, Unix, UTC
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// Date and time of the end of the alert, Unix, UTC
        /// </summary>
        [JsonProperty("end")]
        public int End { get; set; }

        /// <summary>
        /// Description of the alert
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Type of severe weather
        /// </summary>
        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
