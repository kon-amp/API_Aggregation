using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace ApiAggregation.Models
{
    public class NewsData
    {
        [Required(ErrorMessage = "Identifier is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Title is required.")]

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string Url { get; set; }
    }
}
