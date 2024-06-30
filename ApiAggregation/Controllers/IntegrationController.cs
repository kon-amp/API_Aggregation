using Microsoft.AspNetCore.Mvc;
using ApiAggregation.Services;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Models.Github;
using Swashbuckle.AspNetCore.Annotations;
using ApiAggregation.Models;
using ApiAggregation.Services.Integrations;
using ApiAggregation.Models.CountriesInfo;
using Microsoft.AspNetCore.Authorization;

namespace ApiAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IntegrationController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly NewsService _newsService;
        private readonly SpotifyService _spotifyService;
        private readonly GithubService _githubService;
        private readonly CountriesService _countriesService;
        private readonly AggregationService _aggregationService;

        public IntegrationController(WeatherService weatherService, NewsService newsService, SpotifyService spotifyService, GithubService githubService, CountriesService countriesService, AggregationService aggregationService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
            _githubService = githubService;
            _countriesService = countriesService;
            _aggregationService = aggregationService;
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

        [HttpGet("Countries")]
        [SwaggerOperation(Summary = "Countries Informations")]
        public async Task<IActionResult> GetCountries([FromQuery] CountriesInfoRequest request)
        {
            CountriesInfoResponse? response = await _countriesService.GetDataAsync(request);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "All APIs", Description = "Get All Available Apis Simultaneously")]
        public async Task<IActionResult> GetAllApis([FromQuery] WeatherRequest weatherRequest, [FromQuery] NewsRequest newsRequest, [FromQuery] CountriesInfoRequest countriesInfoRequest)
        {
            //var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            AggregatedData? response = await _aggregationService.GetAggregatedDataAsync(weatherRequest, newsRequest, countriesInfoRequest);
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
