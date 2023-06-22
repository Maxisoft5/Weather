using WeatherWatcher.DTO.Enums;

namespace WeatherWatcher.DTO.Filters
{
    public class FilterDTO<T>
    {
        public string Field { get; set; }
        public T Value { get; set; }
        public FilterMatchMode MatchMode { get; set; }
    }
}
