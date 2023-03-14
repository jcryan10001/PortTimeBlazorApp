namespace PortTime.Data
{
    public class Weather
    {
        public string Location { get; set; }
        public string Condition { get; set; }
        public decimal Temperature { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public Dictionary<string, decimal> Forecast { get; set; }
    }

}
