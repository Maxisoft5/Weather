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
    public class WeatherInfoConfiguration : BaseEntityConfig<WeatherInfo>
    {
        public WeatherInfoConfiguration() : base("WeatherInfos")
        {
        }

        public void Configure(EntityTypeBuilder<WeatherInfo> builder)
        {
            builder.HasOne(x => x.LoadArchiveStatus).WithMany(x => x.WeatherInfos);
        }
    }
}
