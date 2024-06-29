using ApiAggregation.Models.Enums;

namespace ApiAggregation.Models.News
{
    /// <summary>
    /// Represents a request to get breaking news headlines from the News API.
    /// </summary>
    public class NewsRequest
    {
        /// <summary>
        /// The country for which to get the top headlines.
        /// </summary>
        public NewsApiCountryEnumOption Country { get; set; } = NewsApiCountryEnumOption.gr;

        /// <summary>
        /// The News API key for authenticating the request.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
