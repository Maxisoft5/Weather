using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.DTO.Filters;
using WeatherWatcher.DTO.Import;

namespace WeatherWatcher.Services.Infrastructure.Services
{
    public interface IWeatherInfoService
    {
        public Task<int> GetTotalRecordsCount(List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null,
            IEnumerable<FilterDTO<TimeOnly>> timeFilters = null);
        public Task<IEnumerable<WeatherInfoDTO>> GetPaginated(int offset, int size,
                List<FilterDTO<string>> stringFilters = null,
                List<FilterDTO<decimal>> decimalFilters = null,
                List<FilterDTO<int>> intFilters = null,
                List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null,
                List<FilterDTO<TimeOnly>> timeFilters = null);
        public Task ImportExcelArchives(ArchiveLoadingDTO archiveLoading);
    }
}
