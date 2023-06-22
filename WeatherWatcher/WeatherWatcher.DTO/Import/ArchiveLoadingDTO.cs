using Microsoft.AspNetCore.Http;

namespace WeatherWatcher.DTO.Import
{
    public class ArchiveLoadingDTO
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
