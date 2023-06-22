using WeatherWatcher.Domain.Entities.Base;
using WeatherWatcher.DTO.Enums;

namespace WeatherWatcher.Domain.Entities
{
    public class LoadArchiveStatus : DbModel
    {
        public LoadStatus LoadStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int RowsCount { get; set; }
        public string? Error { get; set; }
        public IEnumerable<WeatherInfo>? WeatherInfos { get; set; }
    }
}
