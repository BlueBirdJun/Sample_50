using System;

namespace Sample_50
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string desc1 { get; set; }
        public string desc2 { get; set; }

        public string Summary { get; set; }
    }
}
