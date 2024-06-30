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
using ApiAggregation.Services.Interfaces;

namespace ApiAggregation.Controllers
{
    /// <summary>
    /// Controller for handling integration with multiple APIs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IntegrationController : ControllerBase
    {
        private readonly IApiService<WeatherRequest, WeatherResponse> _weatherService;
        private readonly IApiService<NewsRequest, NewsResponse> _newsService;
        private readonly IApiService<SpotifyArtistsRequest, SpotifyResponse> _spotifyService;
        private readonly IApiService<GithubRequest, GithubResponse> _githubService;
        private readonly IApiService<CountriesInfoRequest, CountriesInfoResponse> _countriesService;
        private readonly IAggregationService _aggregationService;


        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationController"/> class.
        /// </summary>
        /// <param name="weatherService">Service for fetching weather data.</param>
        /// <param name="newsService">Service for fetching news data.</param>
        /// <param name="spotifyService">Service for fetching Spotify artist data.</param>
        /// <param name="githubService">Service for fetching GitHub repository data.</param>
        /// <param name="countriesService">Service for fetching country information.</param>
        /// <param name="aggregationService">Service for aggregating data from multiple sources.</param>
        public IntegrationController(
            IApiService<WeatherRequest, WeatherResponse> weatherService,
            IApiService<NewsRequest, NewsResponse> newsService,
            IApiService<SpotifyArtistsRequest, SpotifyResponse> spotifyService,
            IApiService<GithubRequest, GithubResponse> githubService,
            IApiService<CountriesInfoRequest, CountriesInfoResponse> countriesService,
            IAggregationService aggregationService)
        {
            _weatherService = weatherService;
            _newsService = newsService;
            _spotifyService = spotifyService;
            _githubService = githubService;
            _countriesService = countriesService;
            _aggregationService = aggregationService;
        }

        /// <summary>
        /// Gets the current weather data based on the provided request's geographical coordinates.
        /// </summary>
        /// <param name="request">The weather request containing coordinates.</param>
        /// <returns>An <see cref="IActionResult"/> containing the weather data.</returns>
        [HttpGet("Weather")]
        [SwaggerOperation(Summary = "Weather data", Description = "Get the current weather data based on the provided request geographical coordinates")]
        public async Task<IActionResult> GetWeather([FromQuery] WeatherRequest request)
        {
            WeatherResponse? response = await _weatherService.GetDataAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Gets the latest news based on the country.
        /// </summary>
        /// <param name="request">The news request containing country information.</param>
        /// <returns>An <see cref="IActionResult"/> containing the news data.</returns>
        [HttpGet("News")]
        [SwaggerOperation(Summary = "Breaking news", Description = "Get the latest news based on country")]
        public async Task<IActionResult> GetNews([FromQuery] NewsRequest request)
        {
            NewsResponse? response = await _newsService.GetDataAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Gets information about countries.
        /// </summary>
        /// <param name="request">The request for country information.</param>
        /// <returns>An <see cref="IActionResult"/> containing country information.</returns>
        [HttpGet("Countries")]
        [SwaggerOperation(Summary = "Countries Informations")]
        public async Task<IActionResult> GetCountries([FromQuery] CountriesInfoRequest request)
        {
            CountriesInfoResponse? response = await _countriesService.GetDataAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Gets aggregated data from all available APIs simultaneously.
        /// </summary>
        /// <param name="weatherRequest">The weather request containing coordinates.</param>
        /// <param name="newsRequest">The news request containing country information.</param>
        /// <param name="countriesInfoRequest">The request for country information.</param>
        /// <returns>An <see cref="IActionResult"/> containing aggregated data from all APIs.</returns>
        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "All APIs", Description = "Get All Available Apis Simultaneously")]
        public async Task<IActionResult> GetAllApis([FromQuery] WeatherRequest weatherRequest, [FromQuery] NewsRequest newsRequest, [FromQuery] CountriesInfoRequest countriesInfoRequest)
        {
            AggregatedData? response = await _aggregationService.GetAggregatedDataAsync(weatherRequest, newsRequest, countriesInfoRequest);
            return Ok(response);
        }

        /// <summary>
        /// Gets artist information from Spotify based on the Spotify ID.
        /// </summary>
        /// <param name="request">The request containing the Spotify artist ID.</param>
        /// <returns>An <see cref="IActionResult"/> containing the artist information.</returns>
        [HttpGet("Spotify")]
        [SwaggerOperation(Summary = "Artists information", Description = "Get artists information based on spotify id")]
        public async Task<IActionResult> GetSpotify([FromQuery] SpotifyArtistsRequest request)
        {
            SpotifyResponse? response = await _spotifyService.GetDataAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Gets GitHub repositories for a specific user.
        /// </summary>
        /// <param name="request">The request containing the GitHub username.</param>
        /// <returns>An <see cref="IActionResult"/> containing the GitHub repositories.</returns>
        [HttpGet("Github")]
        [SwaggerOperation(Summary = "GitHub repositories", Description = "Get github repositories for specific user")]
        public async Task<IActionResult> GetGithub([FromQuery] GithubRequest request)
        {
            GithubResponse? response = await _githubService.GetDataAsync(request);
            return Ok(response);
        }
    }
}
