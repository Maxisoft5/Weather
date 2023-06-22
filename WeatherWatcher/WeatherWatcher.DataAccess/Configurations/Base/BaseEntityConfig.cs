using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.Domain.Entities.Base;

namespace WeatherWatcher.DataAccess.Configurations.Base
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType>
      where TType : DbModel
    {
        protected string TableName { get; set; }

        public BaseEntityConfig(string tableName)
        {
            TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(obj => obj.Id);

            builder.Property(x => x.AddedTime).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
