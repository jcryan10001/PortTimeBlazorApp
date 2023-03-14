using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;

namespace PortTime.Data
{
    public class CityWeather
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime LocalTime { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public FlatData flatData { get; set; }
    }
}
