using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DTO.EntityDTO;

namespace WeatherWatcher.Services.Infrastructure.Services
{
    public interface ILoadArchiveStatusStervice
    {
        public Task<LoadArchiveStatusDTO> Start(LoadArchiveStatusDTO loadArchiveStatus);
        public Task<LoadArchiveStatusDTO> EndSuccess(LoadArchiveStatusDTO loadArchiveStatus);
        public Task<LoadArchiveStatusDTO> EndError(LoadArchiveStatusDTO loadArchiveStatus, string message);
        public Task<LoadArchiveStatusDTO> UpdateRowsCount(LoadArchiveStatusDTO archiveStatusDTO, int count);
        public Task<IEnumerable<LoadArchiveStatusDTO>> GetAll();
    }
}
