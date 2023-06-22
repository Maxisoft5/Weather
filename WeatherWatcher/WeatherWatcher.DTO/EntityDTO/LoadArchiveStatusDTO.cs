using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DTO.EntityDTO.Base;
using WeatherWatcher.DTO.Enums;

namespace WeatherWatcher.DTO.EntityDTO
{
    public class LoadArchiveStatusDTO : DbModelDTO
    {
        public LoadStatus LoadStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int RowsCount { get; set; }
        public string? Error { get; set; }
        public IEnumerable<WeatherInfoDTO>? WeatherInfos { get; set; }
    }
}
