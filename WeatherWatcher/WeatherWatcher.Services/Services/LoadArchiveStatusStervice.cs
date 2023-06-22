using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.Services.Infrastructure.Repositories;
using WeatherWatcher.Services.Infrastructure.Services;

namespace WeatherWatcher.Services.Services
{
    public class LoadArchiveStatusStervice : ILoadArchiveStatusStervice
    {
        private readonly ILoadArchiveStatusRepository _loadArchiveStatusRepository;
        private readonly ILogger<LoadArchiveStatusStervice> _logger;
        public LoadArchiveStatusStervice(ILoadArchiveStatusRepository loadArchiveStatusRepository, 
            ILogger<LoadArchiveStatusStervice> logger) 
        {
            _loadArchiveStatusRepository = loadArchiveStatusRepository;
            _logger = logger;
        }

        public async Task<LoadArchiveStatusDTO> EndError(LoadArchiveStatusDTO loadArchiveStatus, string message)
        {
            var started = await _loadArchiveStatusRepository.GetAsNotTrackingRowById(loadArchiveStatus.Id);
            if (started == null)
            {
                _logger.LogWarning($"Load archive status with Id {loadArchiveStatus.Id} was not found");
                return null;
            }
            started.EndDateTime = DateTime.UtcNow;
            started.LoadStatus = DTO.Enums.LoadStatus.Failed;
            started.Error = message;
            var ended = await _loadArchiveStatusRepository.UpdateRow(started);
            return new LoadArchiveStatusDTO()
            {
                Id = ended.Id,
                LoadStatus = ended.LoadStatus,
                AddedTime = ended.AddedTime,
                EndDateTime = ended.EndDateTime,
                RowsCount = ended.RowsCount,
                StartDateTime = ended.StartDateTime,
                Error = ended.Error
            };
        }

        public async Task<LoadArchiveStatusDTO> EndSuccess(LoadArchiveStatusDTO loadArchiveStatus)
        {
            var started = await _loadArchiveStatusRepository.GetAsNotTrackingRowById(loadArchiveStatus.Id);
            if (started == null)
            {
                _logger.LogWarning($"Load archive status with Id {loadArchiveStatus.Id} was not found");
                return null;
            }
            started.EndDateTime = DateTime.UtcNow;
            started.LoadStatus = DTO.Enums.LoadStatus.Success;
            var ended = await _loadArchiveStatusRepository.UpdateRow(started);
            return new LoadArchiveStatusDTO()
            {
                Id = ended.Id,
                LoadStatus = ended.LoadStatus,
                AddedTime = ended.AddedTime,
                EndDateTime = ended.EndDateTime,
                RowsCount = ended.RowsCount,
                StartDateTime = ended.StartDateTime
            };
        }

        public async Task<IEnumerable<LoadArchiveStatusDTO>> GetAll()
        {
            var all = await _loadArchiveStatusRepository.GetAllRows();
            var dto = new List<LoadArchiveStatusDTO>();
            foreach (var row in all)
            {
                var status = new LoadArchiveStatusDTO()
                {
                    Id = row.Id,
                    RowsCount= row.RowsCount,
                    AddedTime= row.AddedTime,
                    EndDateTime = row.EndDateTime,
                    StartDateTime= row.StartDateTime,
                    Error = row.Error,
                    LoadStatus = row.LoadStatus
                };
                dto.Add(status);
            }
            return dto;
        }

        public async Task<LoadArchiveStatusDTO> Start(LoadArchiveStatusDTO loadArchiveStatus)
        {
            var saved = await _loadArchiveStatusRepository.CreateRow(new Domain.Entities.LoadArchiveStatus()
            {
                LoadStatus = DTO.Enums.LoadStatus.Started,
                StartDateTime = DateTime.UtcNow,
                RowsCount = loadArchiveStatus.RowsCount
            });
            return new LoadArchiveStatusDTO()
            {
                LoadStatus = saved.LoadStatus,
                StartDateTime = saved.StartDateTime,
                RowsCount = saved.RowsCount,
                AddedTime = saved.AddedTime,
                Id = saved.Id
            };

        }

        public async Task<LoadArchiveStatusDTO> UpdateRowsCount(LoadArchiveStatusDTO archiveStatusDTO, int count)
        {
            var started = await _loadArchiveStatusRepository.GetAsNotTrackingRowById(archiveStatusDTO.Id);
            if (started == null)
            {
                _logger.LogWarning($"Load archive status with Id {archiveStatusDTO.Id} was not found");
                return null;
            }
            started.RowsCount = count;
            var updated = await _loadArchiveStatusRepository.UpdateRow(started);
            return new LoadArchiveStatusDTO()
            {
                Id = updated.Id,
                LoadStatus = updated.LoadStatus,
                AddedTime = updated.AddedTime,
                EndDateTime = updated.EndDateTime,
                RowsCount = updated.RowsCount,
                StartDateTime = updated.StartDateTime
            };
        }
    }
}
