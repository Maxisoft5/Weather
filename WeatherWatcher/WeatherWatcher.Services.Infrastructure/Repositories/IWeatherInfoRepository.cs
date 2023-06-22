using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.DTO.Filters;

namespace WeatherWatcher.Services.Infrastructure.Repositories
{
    public interface IWeatherInfoRepository
    {
        public Task<int> GetTotalRecordsCount(List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null, 
            IEnumerable<FilterDTO<TimeOnly>> timeFilters = null);
        public Task<IEnumerable<WeatherInfo>> GetPaginated(int offset, int size,
                IEnumerable<FilterDTO<string>> stringFilters = null,
                IEnumerable<FilterDTO<decimal>> decimalFilters = null,
                IEnumerable<FilterDTO<int>> intFilters = null,
                IEnumerable<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null,
                IEnumerable<FilterDTO<TimeOnly>> timeFilters = null);
        public Task<IEnumerable<WeatherInfo>> ImportRange(IEnumerable<WeatherInfo> weatherInfos);
    }
}
