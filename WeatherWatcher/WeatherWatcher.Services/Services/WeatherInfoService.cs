using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.BackgroundServices.ImportArchives;
using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.DTO.Enums;
using WeatherWatcher.DTO.Filters;
using WeatherWatcher.DTO.Import;
using WeatherWatcher.Services.Infrastructure.Repositories;
using WeatherWatcher.Services.Infrastructure.Services;

namespace WeatherWatcher.Services.Services
{
    public class WeatherInfoService : IWeatherInfoService
    {
        private readonly IWeatherInfoRepository _repository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ImportArchiveBackgroundTaskQueue _importQueue;
        private readonly ILoadArchiveStatusStervice _loadArchiveStatus;
        public WeatherInfoService(IWeatherInfoRepository weatherInfoRepository, IServiceScopeFactory serviceScopeFactory,
            ImportArchiveBackgroundTaskQueue importQueue, ILoadArchiveStatusStervice archiveStatusStervice) 
        { 
            _loadArchiveStatus = archiveStatusStervice;
            _importQueue = importQueue;
            _repository = weatherInfoRepository;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IEnumerable<WeatherInfoDTO>> GetPaginated(int offset, int size,
            List<FilterDTO<string>> stringFilters = null,
            List<FilterDTO<decimal>> decimalFilters = null,
            List<FilterDTO<int>> intFilters = null, 
            List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null, 
            List<FilterDTO<TimeOnly>> timeFilters = null)
        {
            var weathers = await _repository.GetPaginated(offset, size, stringFilters, decimalFilters, 
                intFilters, dateOnlyFilters, timeFilters);

            var dto = new List<WeatherInfoDTO>();
            foreach (var weather in weathers)
            {
                dto.Add(new WeatherInfoDTO
                {
                    Id = weather.Id,
                    WeatherPhenomena = weather.WeatherPhenomena,
                    WindDirection = weather.WindDirection,
                    WindSpeed = weather.WindSpeed,
                    WindTemperature = weather.WindTemperature,
                    AddedTime = weather.AddedTime,
                    AtmospherePressure = weather.AtmospherePressure,
                    BottomLineCloudiness = weather.BottomLineCloudiness,
                    Cloudiness = weather.Cloudiness,
                    Date = weather.Date,
                    DateOnly = weather.DateOnly,
                    DewPointTemperature = weather.DewPointTemperature,
                    HorizontalVisibility = weather.HorizontalVisibility,
                    RelativeHumidity = weather.RelativeHumidity,
                    TimeOnly = weather.TimeOnly
                });
            }

            return dto;
        }

        public async Task<int> GetTotalRecordsCount(List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null,
            IEnumerable<FilterDTO<TimeOnly>> timeFilters = null)
        {
            return await _repository.GetTotalRecordsCount(dateOnlyFilters: dateOnlyFilters, timeFilters: timeFilters);
        }
        public async Task ImportExcelArchives(ArchiveLoadingDTO archiveLoading)
        {
            var streams = new List<Stream>();
            foreach (var file in archiveLoading.Files)
            {
                var stream = new MemoryStream();
                file.CopyTo(stream);
                stream.Position = 0;
                streams.Add(stream);
            }
            var status = new LoadArchiveStatusDTO();
            status = await _loadArchiveStatus.Start(status);
            

            _importQueue.QueueBackgroundWorkItem(async (_) =>
            {
                var sw = new Stopwatch();
                sw.Start();
                ILoadArchiveStatusStervice loadStatus = null;
                ILogger<WeatherInfoService> loggerService = null;
                var weathersInfos = new List<WeatherInfo>();

                try
                {
                    using var scope = _serviceScopeFactory.CreateAsyncScope();
                    loggerService = scope.ServiceProvider.GetRequiredService<ILogger<WeatherInfoService>>();
                    var weatherRepo = scope.ServiceProvider.GetRequiredService<IWeatherInfoRepository>();
                    loadStatus = scope.ServiceProvider.GetRequiredService<ILoadArchiveStatusStervice>();
                 
                    loggerService.LogInformation($"Started import to memory of {archiveLoading.Files.Count()} excel files");
                    foreach (var stream in streams)
                    {
                        var book = new XSSFWorkbook(stream);
                        for (int i = 0; i < book.NumberOfSheets - 1; i++)
                        {
                            var sheet = book.GetSheetAt(i);
                            for (int j = 0; j < sheet.LastRowNum; j++)
                            {
                                if (j == 0 || j == 1 || j == 2 || j == 3)
                                {
                                    continue;
                                }
                                var row = sheet.GetRow(j);
                                var weather = new WeatherInfo();
                                var date = row.Cells.Count >= 1 ? DateTime.Parse(row.Cells[0].StringCellValue) : default;
                                var time = row.Cells.Count >= 2 ? TimeOnly.Parse(row.Cells[1].StringCellValue) : default;
                                date = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
                                weather.Date = date;
                                weather.WindTemperature = row.Cells[2].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 3
                                        ? row.Cells[2].NumericCellValue : 0;
                                weather.RelativeHumidity = row.Cells[3].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 4
                                    ? (int)row.Cells[3].NumericCellValue : 0;
                                weather.DewPointTemperature = row.Cells[4].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 5 ?
                                    row.Cells[4].NumericCellValue : 0;
                                weather.AtmospherePressure = row.Cells[5].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 6
                                    ? (int)row.Cells[5].NumericCellValue : 0;
                                weather.WindDirection = row.Cells.Count >= 7 ? row.Cells[6].StringCellValue : "";
                                weather.WindSpeed = row.Cells[7].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 8
                                    ? (int)row.Cells[7].NumericCellValue : 0;
                                weather.Cloudiness = row.Cells[8].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 9 ?
                                    (int)row.Cells[8].NumericCellValue : 0;
                                weather.BottomLineCloudiness = row.Cells[9].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 10 ?
                                    (int)row.Cells[9].NumericCellValue : 0;
                                weather.HorizontalVisibility = row.Cells[10].CellType == NPOI.SS.UserModel.CellType.Numeric && row.Cells.Count >= 11 ?
                                    (int)row.Cells[10].NumericCellValue : 0;
                                weather.WeatherPhenomena = row.Cells.Count == 12 ? row.Cells[11].StringCellValue : "";

                                weathersInfos.Add(weather);
                            }
                        }
                    }
                    status.RowsCount = weathersInfos.Count();
                    status = await loadStatus.UpdateRowsCount(status, weathersInfos.Count());
                    loggerService.LogInformation($"Started import to database of {weathersInfos.Count()} rows");
                    var saved = await weatherRepo.ImportRange(weathersInfos);
                    loggerService.LogInformation($"Archive import was ended successfully, took ${sw.Elapsed} time, imported count ${saved.Count()}");
                    sw.Stop();
                    status = await loadStatus.EndSuccess(status);
                }
                catch (IOException ex)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    loadStatus = scope.ServiceProvider.GetRequiredService<ILoadArchiveStatusStervice>();
                    loggerService.LogError(ex, "Error with reading or accessing of excel files");
                    await loadStatus.EndError(status, ex.Message);
                }
                catch (IndexOutOfRangeException ex)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    loadStatus = scope.ServiceProvider.GetRequiredService<ILoadArchiveStatusStervice>();
                    loggerService.LogError(ex, "Error during reading of excel rows");
                    await loadStatus.EndError(status, ex.Message);
                }
                catch (Exception ex)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    loadStatus = scope.ServiceProvider.GetRequiredService<ILoadArchiveStatusStervice>();
                    loggerService.LogError(ex, "Error in archive import");
                    await loadStatus.EndError(status, ex.Message);
                }
              
            });
        }
    }
}
