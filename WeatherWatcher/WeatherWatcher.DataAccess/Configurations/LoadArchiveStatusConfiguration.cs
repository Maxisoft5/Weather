using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DataAccess.Configurations.Base;
using WeatherWatcher.Domain.Entities;

namespace WeatherWatcher.DataAccess.Configurations
{
    public class LoadArchiveStatusConfiguration : BaseEntityConfig<LoadArchiveStatus>
    {
        public LoadArchiveStatusConfiguration() : base("LoadarchiveStatuses")
        {
        }

        public void Configure(EntityTypeBuilder<LoadArchiveStatus> builder)
        {
            builder.HasMany(x => x.WeatherInfos).WithOne(x => x.LoadArchiveStatus).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
