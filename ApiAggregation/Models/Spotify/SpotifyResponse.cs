using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models.Spotify
{
    /// <summary>
    /// Represents the response from the spotify API.
    /// </summary>
    public class SpotifyResponse
    {
        /// <summary>
        /// A set of artists
        /// </summary>
        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; } = new List<Artist>();
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Artist
    {
        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls External_urls { get; set; } = new ExternalUrls();

        /// <summary>
        /// Information about the followers of the artist.
        /// </summary>
        [JsonProperty("followers")]
        public Followers Followers { get; set; } = new Followers();

        /// <summary>
        /// A list of the genres the artist is associated with. 
        /// If not yet classified, the array is empty.
        /// </summary>
        [JsonProperty("genres")]
        public List<string> Genres { get; set; } = new List<string>();

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        [JsonProperty("href")]
        public string? Href { get; set; }

        /// <summary>
        /// The Spotify ID for the artist.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        [JsonProperty("images")]
        public List<Image> Images { get; set; } = new List<Image>();

        /// <summary>
        /// The name of the artist.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular. 
        /// The artist's popularity is calculated from the popularity of all the artist's tracks.
        /// </summary>
        [JsonProperty("name")]
        [Range(0, 100, ErrorMessage = "Popularity can be between 0 to 100.It can be though as percent.")]
        public int Popularity { get; set; }

        /// <summary>
        /// Allowed values: "artist"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;
    }

    public class ExternalUrls
    {
        /// <summary>
        /// The Spotify URL for the object.
        /// </summary>
        [JsonProperty("uri")]
        public string Spotify { get; set; } = string.Empty;
    }

    public class Followers
    {
        /// <summary>
        /// This will always be set to null, as the Web API does not support it at the moment.
        /// </summary>
        [JsonProperty("href")]
        public string? Href { get; set; }

        /// <summary>
        /// The total number of followers.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Image
    {
        /// <summary>
        /// The source URL of the image.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// The image height in pixels.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// The image width in pixels.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
    }

}
