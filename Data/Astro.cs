using System.Text.Json.Serialization;

namespace PortTime.Data
{
    public class Astro
    {
        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; }

        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
        public int is_moon_up { get; set; }
        public int is_sun_up { get; set; }
    }
}
