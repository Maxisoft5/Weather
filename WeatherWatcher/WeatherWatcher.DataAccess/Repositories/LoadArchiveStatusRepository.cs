using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DataAccess.EFCore;
using WeatherWatcher.Domain.Entities;
using WeatherWatcher.Services.Infrastructure.Repositories;

namespace WeatherWatcher.DataAccess.Repositories
{
    public class LoadArchiveStatusRepository : ILoadArchiveStatusRepository
    {

        private readonly DataContext _dataContext;
        public LoadArchiveStatusRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<LoadArchiveStatus> CreateRow(LoadArchiveStatus loadArchive)
        {
            await _dataContext.LoadArchiveStatuses.AddAsync(loadArchive);
            await _dataContext.SaveChangesAsync();
            return loadArchive;
        }

        public async Task<IEnumerable<LoadArchiveStatus>> GetAllRows()
        {
            return await _dataContext.LoadArchiveStatuses.ToListAsync();
        }

        public async Task<LoadArchiveStatus> GetAsNotTrackingRowById(long id)
        {
            return await _dataContext.LoadArchiveStatuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LoadArchiveStatus> UpdateRow(LoadArchiveStatus loadArchive)
        {
            var local = _dataContext.Set<LoadArchiveStatus>()
                .Local.FirstOrDefault(entry => entry.Id.Equals(loadArchive.Id));

            if (local != null)
            {
                local.Error = loadArchive.Error;
                local.StartDateTime = loadArchive.StartDateTime;
                local.EndDateTime = loadArchive.EndDateTime;
                local.LoadStatus = loadArchive.LoadStatus;
                local.RowsCount = loadArchive.RowsCount;
                _dataContext.Entry(local).State = EntityState.Modified;
            } else
            {
                _dataContext.LoadArchiveStatuses.Update(loadArchive);
            }

            await _dataContext.SaveChangesAsync();
            return loadArchive;
        }
    }
}
