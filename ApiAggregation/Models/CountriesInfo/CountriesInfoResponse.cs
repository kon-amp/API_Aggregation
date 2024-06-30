using Newtonsoft.Json;

namespace ApiAggregation.Models.CountriesInfo
{
    /// <summary>
    /// Represents the response containing information about multiple countries.
    /// </summary>
    public class CountriesInfoResponse
    {
        /// <summary>
        /// The list of countries information.
        /// </summary>
        public List<CountriesInfo> CountriesInfo { get; set; } = new List<CountriesInfo>();
    }

    /// <summary>
    /// Represents detailed information about a country.
    /// </summary>
    public class CountriesInfo
    {
        [JsonProperty("capital")]
        public List<string> Capital { get; set; } = new List<string>();

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("subregion")]
        public string Subregion { get; set; } = string.Empty;
    }
}
