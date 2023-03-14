using System.Text.Json.Serialization;

namespace PortTime.Data
{
    public class CurrentWeatherResponse
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }
}
