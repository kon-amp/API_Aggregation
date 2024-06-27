using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiAggregation.Services;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Models;

namespace ApiAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly AggregationService _aggregationService;

        public AggregationController(AggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        // TO DO - Change parameters in a more abstract way 
        [HttpGet]
        public async Task<IActionResult> GetAggregatedData([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] string country, [FromQuery] string apiKey, [FromQuery] string spotifyIds)
        {
            var weatherRequest = new WeatherRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                ApiKey = apiKey
            };

            var newsRequest = new NewsRequest
            {
                Country = country,
                ApiKey = apiKey
            };

            var spotifyRequest = new SpotifyArtistsRequest
            {
                Ids = spotifyIds
            };

            AggregatedData? data = await _aggregationService.GetAggregatedDataAsync(weatherRequest, newsRequest, spotifyRequest);
            return Ok(data);
        }
    }
}
