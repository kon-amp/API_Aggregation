using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models
{
    public class SpotifyData
    {
        [Required(ErrorMessage = "Identifier is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        public string Artist { get; set; }

        public string[] AlbumNames { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string Url { get; set; }

        public int Popularity { get; set; }
    }
}
