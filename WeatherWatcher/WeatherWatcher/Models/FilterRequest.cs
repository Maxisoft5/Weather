using WeatherWatcher.DTO.Filters;

namespace WeatherWatcher.Models
{
    public class FilterRequest
    {
        public int Offset { get; set; }
        public int Size { get; set; }
        public string DateOnlyFilters { get; set; }
        public string TimeOnlyFilters { get; set; }
    }
}
