using System.ComponentModel.DataAnnotations.Schema;
using WeatherWatcher.DTO.EntityDTO.Base;

namespace WeatherWatcher.DTO.EntityDTO
{
    public class WeatherInfoDTO : DbModelDTO
    {
        public DateOnly DateOnly { get; set; }
        public TimeOnly TimeOnly { get; set; }
        public DateTime Date { get; set; }
        public double WindTemperature { get; set; }
        public int RelativeHumidity { get; set; }
        public double DewPointTemperature { get; set; }
        public int AtmospherePressure { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        public int BottomLineCloudiness { get; set; }
        public int HorizontalVisibility { get; set; }
        public string WeatherPhenomena { get; set; }
    }
}
