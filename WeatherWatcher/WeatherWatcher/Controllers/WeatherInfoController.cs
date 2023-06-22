using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.DTO.Filters;
using WeatherWatcher.DTO.Import;
using WeatherWatcher.Models;
using WeatherWatcher.Services.Infrastructure.Services;

namespace WeatherWatcher.Controllers
{
    public class WeatherInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherInfoService _weatherInfoService;
        private readonly ILoadArchiveStatusStervice _loadArchiveStatusStService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherInfoController(ILogger<HomeController> logger, IWeatherInfoService infoService,
            ILoadArchiveStatusStervice loadArchiveStatusStervice,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _loadArchiveStatusStService = loadArchiveStatusStervice;
            _weatherInfoService = infoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedTable(FilterRequest filterReq)
        {
            var dates = new List<FilterDTO<RangeDateFilterDTO>>();
            if (filterReq.DateOnlyFilters != null)
            {
                var parsedRangeDates = JsonConvert.DeserializeObject<List<DateTime>>(filterReq.DateOnlyFilters);
                var range = new RangeDateFilterDTO()
                {
                    From = parsedRangeDates[0],
                    To = parsedRangeDates[1]
                };
                if (parsedRangeDates != null)
                {
                    dates.Add(new FilterDTO<RangeDateFilterDTO>()
                    {
                        Field = "Date",
                        MatchMode = DTO.Enums.FilterMatchMode.Equals,
                        Value = range
                    });
                }
            }
            var times = new List<FilterDTO<TimeOnly>>();
            if(filterReq.TimeOnlyFilters != null)
            {
                var time = JsonConvert.DeserializeObject<TimeOnly>(filterReq.TimeOnlyFilters);
                if (time != null)
                {
                    times.Add(new FilterDTO<TimeOnly>()
                    {
                        Field = "Date",
                        MatchMode = DTO.Enums.FilterMatchMode.Equals,
                        Value = time
                    });
                }
            }

            var weathers = await _weatherInfoService.GetPaginated(filterReq.Offset, filterReq.Size, 
                dateOnlyFilters: dates, timeFilters:times  );
            foreach ( var weather in weathers )
            {
                weather.TimeOnly = new TimeOnly(weather.Date.Hour, weather.Date.Minute, weather.Date.Second);
                weather.DateOnly = new DateOnly(weather.Date.Year, weather.Date.Month, weather.Date.Day);
            }
            var totalCount = await _weatherInfoService.GetTotalRecordsCount(dateOnlyFilters: dates, timeFilters: times);
            int numberOfLinks = totalCount / filterReq.Size;
            var rest = totalCount % filterReq.Size;
            if (rest != 0)
            {
                numberOfLinks += 1;
            }
            var currentLink = filterReq.Offset / filterReq.Size;
            var dto = new TableFilterPaginatorDTO()
            {
                WeatherInfos = weathers.ToList(),
                TotalLinks = numberOfLinks,
                CurrentLinkNumber = currentLink
            };
            if (numberOfLinks <= 5)
            {
                dto.Start = 0;
                dto.End = numberOfLinks;
            }
            else
            {
                if (currentLink < 4)
                {
                    dto.Start = 0;
                    dto.End = 5;
                }
                else
                {
                    if ((numberOfLinks - currentLink) <= 2)
                    {
                        dto.End = numberOfLinks;
                        dto.Start = numberOfLinks - 5;
                    }
                    else
                    {
                        dto.Start = currentLink - 2;
                        dto.End = currentLink + 3;
                    }
                }
            }
            return PartialView("_TableBody", dto);
        }


        [HttpPost]
        public async Task<IActionResult> ArchiveLoading()
        {
            var files = _httpContextAccessor.HttpContext.Request.Form.Files;
            var dto = new ArchiveLoadingDTO() { Files = files };
            await _weatherInfoService.ImportExcelArchives(dto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetStatusTable()
        {
            var all = await _loadArchiveStatusStService.GetAll();
            return PartialView("_TableStatus", all);
        }

        public async Task<IActionResult> LoadStatuses()
        {
            var all = await _loadArchiveStatusStService.GetAll();
            return View(all);
        }

    }
}
