using ApiAggregation.Controllers;
using ApiAggregation.Models;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiAggregation.Tests.ControllersTest
{
    [TestClass]
    public class IntegrationControllerTests
    {
        private Mock<IApiService<WeatherRequest, WeatherResponse>> _mockWeatherService = null!;
        private Mock<IApiService<NewsRequest, NewsResponse>> _mockNewsService = null!;
        private Mock<IApiService<SpotifyArtistsRequest, SpotifyResponse>> _mockSpotifyService = null!;
        private Mock<IApiService<GithubRequest, GithubResponse>> _mockGithubService = null!;
        private Mock<IApiService<CountriesInfoRequest, CountriesInfoResponse>> _mockCountriesService = null!;
        private Mock<IAggregationService> _mockAggregationService = null!;
        private IntegrationController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            // Initialize all the mocks
            _mockWeatherService = new Mock<IApiService<WeatherRequest, WeatherResponse>>();
            _mockNewsService = new Mock<IApiService<NewsRequest, NewsResponse>>();
            _mockSpotifyService = new Mock<IApiService<SpotifyArtistsRequest, SpotifyResponse>>();
            _mockGithubService = new Mock<IApiService<GithubRequest, GithubResponse>>();
            _mockCountriesService = new Mock<IApiService<CountriesInfoRequest, CountriesInfoResponse>>();
            _mockAggregationService = new Mock<IAggregationService>();

            // Initialize the controller
            _controller = new IntegrationController(
                _mockWeatherService.Object,
                _mockNewsService.Object,
                _mockSpotifyService.Object,
                _mockGithubService.Object,
                _mockCountriesService.Object,
                _mockAggregationService.Object);
        }

        [TestMethod]
        public async Task GetWeather_ReturnsOk()
        {
            // Arrange
            var request = new WeatherRequest();
            var response = new WeatherResponse();
            _mockWeatherService.Setup(s => s.GetDataAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetWeather(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [TestMethod]
        public async Task GetNews_ReturnsOk()
        {
            // Arrange
            var request = new NewsRequest();
            var response = new NewsResponse();
            _mockNewsService.Setup(s => s.GetDataAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetNews(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [TestMethod]
        public async Task GetCountries_ReturnsOk()
        {
            // Arrange
            var request = new CountriesInfoRequest();
            var response = new CountriesInfoResponse();
            _mockCountriesService.Setup(s => s.GetDataAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetCountries(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [TestMethod]
        public async Task GetAllApis_ReturnsOk()
        {
            // Arrange
            var weatherRequest = new WeatherRequest();
            var newsRequest = new NewsRequest();
            var countriesInfoRequest = new CountriesInfoRequest();
            var response = new AggregatedData();

            _mockAggregationService.Setup(s => s.GetAggregatedDataAsync(weatherRequest, newsRequest, countriesInfoRequest))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllApis(weatherRequest, newsRequest, countriesInfoRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }
    }
}
