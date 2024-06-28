using Microsoft.AspNetCore.Mvc;
using ApiAggregation.Services;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Models.Github;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly NewsService _newsService;
        private readonly SpotifyService _spotifyService;
        private readonly GithubService _githubService;

        public AggregationController(WeatherService weatherService, NewsService newsService, SpotifyService spotifyService, GithubService githubService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
            _githubService = githubService;
        }

        [HttpGet("Weather")]
        [SwaggerOperation(Summary = "Weather data", Description = "Get the current weather data based on the provided request geographical coordinates")]
        public async Task<IActionResult> GetWeather([FromQuery] WeatherRequest request)
        {
            WeatherResponse? response = await _weatherService.GetDataAsync(request);
            return Ok(response);
        }

        [HttpGet("News")]
        [SwaggerOperation(Summary = "Breaking news", Description = "Get the latest news based on country")]
        public async Task<IActionResult> GetNews([FromQuery] NewsRequest request)
        {
            NewsResponse? response = await _newsService.GetDataAsync(request);
            return Ok(response);
        }

        [HttpGet("Spotify")]
        [SwaggerOperation(Summary = "Artists information", Description = "Get artists information based on spotify id")]
        public async Task<IActionResult> GetSpotify([FromQuery] SpotifyArtistsRequest request)
        {
            SpotifyResponse? response = await _spotifyService.GetDataAsync(request);
            return Ok(response);
        }

        [HttpGet("Github")]
        [SwaggerOperation(Summary = "GitHub repositories", Description = "Get github repositories for specific user")]
        public async Task<IActionResult> GetGithub([FromQuery] GithubRequest request)
        {
            GithubResponse? response = await _githubService.GetDataAsync(request);
            return Ok(response);
        }
        
    }
}
