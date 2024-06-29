using Newtonsoft.Json;

namespace ApiAggregation.Models.CountriesInfo
{
    public class CountriesInfoResponse
    {
        public List<CountriesInfo> CountriesInfo { get; set; } = new List<CountriesInfo>();
    }

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
