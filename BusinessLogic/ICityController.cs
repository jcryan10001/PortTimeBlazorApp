using Microsoft.AspNetCore.Mvc;
using PortTime.Data;

namespace PortTime.BusinessLogic
{
    public interface ICityController
    {
        Task<ActionResult<CityWeather>> GetWeather(string cityName);
        Task<ActionResult<List<City>>> GetCityData();
        Task<ActionResult<List<WeatherForecast>>> GetweatherData(string cityName);
    }
}
