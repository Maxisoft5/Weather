using Microsoft.EntityFrameworkCore;
using WeatherWatcher.Domain.Entities;

namespace WeatherWatcher.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
         
        }

        public DbSet<WeatherInfo> WeatherInfos { get; set; }
        public DbSet<LoadArchiveStatus> LoadArchiveStatuses { get; set; }
    }
}
