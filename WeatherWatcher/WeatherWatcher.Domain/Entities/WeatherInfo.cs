using System.ComponentModel.DataAnnotations.Schema;
using WeatherWatcher.Domain.Entities.Base;

namespace WeatherWatcher.Domain.Entities
{
    public class WeatherInfo : DbModel
    {
        [NotMapped]
        public DateOnly DateOnly { get; set; }
        [NotMapped]
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
        public long? LoadArchiveStatusId { get; set; }
        public LoadArchiveStatus? LoadArchiveStatus { get; set; }
    }
}
