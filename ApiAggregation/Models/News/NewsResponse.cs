using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models.News
{
    public class NewsResponse
    {
        /// <summary>
        /// Status of the API response.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Total number of results (total breaking news headlines)
        /// </summary>
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        /// <summary>
        /// List of articles.
        /// </summary>
        [JsonProperty("articles")]
        public List<Article> Articles { get; set; } = new List<Article>();
    }

    public class Article
    {
        /// <summary>
        /// Source of the article.
        /// </summary>
        [JsonProperty("source")]
        public Source Source { get; set; } = new Source();

        /// <summary>
        /// Author of the article.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Title of the article.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the article.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// URL to the article.
        /// </summary>
        [JsonProperty("url")]
        [Url(ErrorMessage = "The URL is not valid.")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// URL to the image of the article.
        /// </summary>
        [JsonProperty("urlToImage")]
        public string? UrlToImage { get; set; }

        /// <summary>
        /// Published date of the article.
        /// </summary>
        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        /// <summary>
        /// Content of the article.
        /// </summary>
        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    public class Source
    {
        /// <summary>
        /// ID of the source.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Name of the source.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}

