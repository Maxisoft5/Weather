using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DTO.EntityDTO;

namespace WeatherWatcher.DTO.Filters
{
    public class TableFilterPaginatorDTO
    {
        public int TotalLinks { get; set; }
        public int CurrentLinkNumber { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public List<WeatherInfoDTO> WeatherInfos { get; set; }
    }
}
