using System.ComponentModel.DataAnnotations;

namespace ApiAggregation.Models
{
    public class WeatherData
    {
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        public string Area{ get; set; }

        [Required(ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }

        [Range(-100, 100, ErrorMessage = "Temperature must be between -100 and 100.")]
        public float Temperature { get; set; }
    }
}
