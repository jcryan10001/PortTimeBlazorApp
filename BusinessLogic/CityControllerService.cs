using Microsoft.AspNetCore.Mvc.Core;
using Newtonsoft.Json.Linq;
using PortTime.Data;
using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Hl7.Fhir.Utility;

namespace PortTime.BusinessLogic
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityControllerService : ICityController
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public CityControllerService(IServiceProvider serviceProvider, IConfiguration config)
        {
            _httpClient = new HttpClient();
            _serviceProvider = serviceProvider;
            _config = config;
        }

        private HttpClient _httpClient = new HttpClient();

        private string WeatherApiKey => _config.GetValue<string>("AppSettings:WeatherApiKey");//api key for weatherapi.com


        // This method is used to retrieve the weather data for a specific city by making a request to the weather API
        // It retrieves the current weather data as well as the astronomy data (sunrise and sunset times)
        // The JSON responses are deserialized into C# objects using the System.Text.Json library
        // The current weather and astronomy data are combined into a single object of type CityWeather and returned as an ActionResult
        [HttpGet("GetWeather")]
        public async Task<ActionResult<CityWeather>> GetWeather(string cityName)
        {
            try
            {
                var currentWeatherResponse = await _httpClient.GetAsync($"http://api.weatherapi.com/v1/current.json?key={WeatherApiKey}&q={cityName}&aqi=no");
                currentWeatherResponse.EnsureSuccessStatusCode();
                var currentWeatherJson = await currentWeatherResponse.Content.ReadAsStringAsync();
                var currentWeatherData = JsonSerializer.Deserialize<CurrentWeatherResponse>(currentWeatherJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var astronomyResponse = await _httpClient.GetAsync($"http://api.weatherapi.com/v1/astronomy.json?key={WeatherApiKey}&q={cityName}&dt={currentWeatherData.Current.LastUpdated:yyyy-MM-dd}");
                astronomyResponse.EnsureSuccessStatusCode();
                var astronomyJson = await astronomyResponse.Content.ReadAsStringAsync();
                var astroData = JsonSerializer.Deserialize<AstronomyResponse>(astronomyJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var flatData = new FlatData
                {
                    CurrentData = new CurrentData
                    {
                        Location = new List<Location> { currentWeatherData.Location },
                        Current = new List<Current> { currentWeatherData.Current }
                    },
                    AstronomyData = new AstronomyData
                    {
                        Astronomies = new List<Astronomy> { astroData.Astronomy }
                    }
                };

                var localTimeStr = currentWeatherData.Location.localtime;
                var sunriseStr = astroData.Astronomy.Astro.Sunrise;
                var sunsetStr = astroData.Astronomy.Astro.Sunset;
                DateTime localTime = DateTime.Parse(localTimeStr);
                var sunSet = DateTime.ParseExact(sunsetStr, "hh:mm tt", CultureInfo.InvariantCulture);
                var sunRise = DateTime.ParseExact(sunriseStr, "hh:mm tt", CultureInfo.InvariantCulture);

                var cityWeather = new CityWeather
                {
                    City = currentWeatherData.Location.Name,
                    Region = currentWeatherData.Location.Region,
                    Country = currentWeatherData.Location.Country,
                    LocalTime = localTime,
                    Temperature = currentWeatherData.Current.TempC,
                    Sunrise = sunRise,
                    Sunset = sunSet,
                    flatData = flatData
                };

                return new OkObjectResult(cityWeather) { StatusCode = 200 };
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new StatusCodeResult(404);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(404);
            }
        }



        [HttpGet("GetCityData")] // HTTP GET method that maps to the endpoint /api/Parts/GetCities
        public async Task<ActionResult<List<City>>> GetCityData()
        {
            var cities = new List<City>();
            var countryUrl = "https://restcountries.com/v3.1/all"; // URL to fetch country data

            try
            {
                var countryResponse = await _httpClient.GetAsync(countryUrl); // Send a GET request to the country URL and wait for a response
                countryResponse.EnsureSuccessStatusCode(); // Throw an exception if the response status code is not successful (i.e., 200-299)
                var countryJson = await countryResponse.Content.ReadAsStringAsync(); // Read the response body as a string

                var countries = JArray.Parse(countryJson); // Parse the JSON response as a JArray

                foreach (var country in countries) // Loop through each country in the JArray
                {
                    var countryCode = country["cca2"].ToString(); // Extract the country code from the country object
                    var capital = country["capital"]; // Extract the capital city object from the country object

                    if (!string.IsNullOrEmpty(capital?.ToString())) // Check if the capital city object exists and is not null or empty
                    {
                        var cityObj = new City // Create a new City object using the capital city data
                        {
                            Name = capital[0].ToString(), // Set the city name
                            ISO = countryCode, // Set the country ISO code
                            Country = country["name"]["common"].ToString(), // Set the country name
                            FlagUrl = $"https://flagcdn.com/128x96/{countryCode.ToLower()}.png" // Set the URL for the country flag image
                        };

                        cities.Add(cityObj); // Add the new City object to the list of cities
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Handle any exceptions by writing the error message to the console
               return new StatusCodeResult(404);
            }

            return new OkObjectResult(cities) { StatusCode = 200 };
        }

        /// <summary>
        /// Makes a request to the Weather API to retrieve the forecast for the specified city for the next 7 days,
        /// and parses the response JSON into a list of WeatherForecast objects.
        /// </summary>
        /// <param name="cityName">The name of the city to retrieve the weather forecast for.</param>
        /// <returns>A list of WeatherForecast objects containing the weather forecast data for the next 7 days.</returns>

        [HttpGet("GetweatherData")]
        public async Task<ActionResult<List<WeatherForecast>>> GetweatherData(string cityName)
        {
            var forecasts = new List<WeatherForecast>();
            try
            {
                // Build the URL for the Weather API forecast endpoint
                var currentWeatherUrl = $"https://api.weatherapi.com/v1/forecast.json?key={WeatherApiKey}&q={cityName}&days=7";
                // Make a request to the forecast API endpoint
                var currentWeatherResponse = await _httpClient.GetAsync(currentWeatherUrl);
                currentWeatherResponse.EnsureSuccessStatusCode();

                // Read the response JSON and parse it into a JObject
                var currentWeatherJson = await currentWeatherResponse.Content.ReadAsStringAsync();
                var data = JObject.Parse(currentWeatherJson);
                // Extract the forecast data for the next 7 days
                var forecastDays = data["forecast"]["forecastday"].Take(7);


                // Parse the forecast data into a list of WeatherForecast objects
                foreach (var forecastDay in forecastDays)
                {
                    var date = DateTime.Parse(forecastDay["date"].ToString());
                    var dayOfWeek = date.ToString("ddd");
                    var minTempC = forecastDay["day"]["mintemp_c"].Value<int>();
                    var maxTempC = forecastDay["day"]["maxtemp_c"].Value<int>();
                    var conditionText = forecastDay["day"]["condition"]["text"].ToString();
                    var imageurl = forecastDay["day"]["condition"]["icon"].ToString();

                    forecasts.Add(new WeatherForecast
                    {
                        DayOfWeek = dayOfWeek,
                        MinTempC = minTempC,
                        MaxTempC = maxTempC,
                        ConditionText = conditionText,
                        ImageUrl = imageurl.ToString(),
                    });
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return forecasts;
        }
    }

}
