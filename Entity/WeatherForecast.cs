using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        [Display(Name = "Summary")]
        public string Summary { get; set; }
        public string X { get; set; }
    }
}