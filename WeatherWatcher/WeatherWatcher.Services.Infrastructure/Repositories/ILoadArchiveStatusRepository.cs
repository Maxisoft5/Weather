using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.Domain.Entities;

namespace WeatherWatcher.Services.Infrastructure.Repositories
{
    public interface ILoadArchiveStatusRepository
    {
        public Task<LoadArchiveStatus> GetAsNotTrackingRowById(long id);
        public Task<LoadArchiveStatus> CreateRow(LoadArchiveStatus loadArchive);
        public Task<LoadArchiveStatus> UpdateRow(LoadArchiveStatus loadArchive);
        public Task<IEnumerable<LoadArchiveStatus>> GetAllRows();
    }
}
