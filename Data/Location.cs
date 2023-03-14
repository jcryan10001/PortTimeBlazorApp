using System.Text.Json.Serialization;

namespace PortTime.Data
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public string localtime { get; set; }
    }
}
