using Newtonsoft.Json.Linq;

namespace PortTime.Data
{
    public class FlatData
    {
        public CurrentData CurrentData { get; set; }
        public AstronomyData AstronomyData { get; set; }
    }

    public class CurrentData
    {
        public List<Location> Location { get; set; }
        public List<Current> Current { get; set; }
    }
    public class AstronomyData
    {
        public List<Astronomy> Astronomies  { get; set; }
    }
}
