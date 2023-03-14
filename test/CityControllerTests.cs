
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using PortTime.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortTime.BusinessLogic;
using System.Net;


namespace PortTime.Tests
{
    public class CityControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IWebHostEnvironment> _mockEnvironment = new Mock<IWebHostEnvironment>();
        private readonly IWebHostEnvironment environment;
        private readonly ICityController controller;
        private readonly CityControllerService _cityController;
        private readonly IConfiguration _config;

        public CityControllerTests()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var config = new Mock<IConfiguration>();
            _cityController = new CityControllerService(serviceProviderMock.Object,config.Object);
        }



        [Fact]
        public async Task GetWeather_Returns_Success()
        {
            // Arrange
            var cityName = "London";

            // Act
            var result = await _cityController.GetWeather(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<CityWeather>>(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetCityDataAsync_Returns_Success()
        {

            // Act
            var result = await _cityController.GetCityData();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<List<City>>>(result);
        }

        [Fact]
        public async Task ParseWeatherForecast_Returns_Success()
        {
            // Arrange
            var cityName = "London";

            // Act
            var result = await _cityController.GetweatherData(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<List<WeatherForecast>>>(result);
        }
        //test for an invalid city name: Write a test that verifies that the GetWeather method returns a BadRequest response when an invalid city name is provided.
        [Fact]
        public async Task GetWeather_Returns_NotFound_For_Invalid_CityName()
        {
            // Arrange
            var cityName = "InvalidCityName";

            // Act
            var result = await _cityController.GetWeather(cityName);

            // Assert
            var notFoundResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        //integration tests
        [Fact]
        public async Task GetWeather_ReturnsCityWeather()
        {
            // Arrange
            var cityName = "New York";
          

            // Act
            var result = await _cityController.GetWeather(cityName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var cityWeather = Assert.IsType<CityWeather>(okResult.Value);

            Assert.Equal(cityName, cityWeather.City);
            Assert.NotNull(cityWeather.Region);
            Assert.NotNull(cityWeather.Country);
            Assert.NotNull(cityWeather.LocalTime);
            Assert.NotNull(cityWeather.Temperature);
            Assert.NotNull(cityWeather.Sunrise);
            Assert.NotNull(cityWeather.Sunset);
            Assert.NotNull(cityWeather.flatData);
        }

        [Fact]
        public async Task GetCityData_ReturnsListOfCities()
        {
            // Arrange

            // Act
            var result = await _cityController.GetCityData();

            // Assert
            Assert.IsType<ActionResult<List<City>>>(result);

        }

        [Fact]
        public async Task GetweatherData_ReturnsWeatherForecast()
        {
            // Arrange
            var cityName = "New York";

            // Act
            var result = await _cityController.GetweatherData(cityName);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var weatherForecasts = Assert.IsType<ActionResult<List<WeatherForecast>>>(result);
            Assert.NotEmpty(weatherForecasts.Value);
            Assert.Equal(7, weatherForecasts.Value.Count);
        }



    }
}
