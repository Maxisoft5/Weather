using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.BackgroundServices.ImportArchives;

namespace WeatherWatcher.BackgroundServices.Extensions
{
    public static class DIContainerExtensions
    {
        public static void AddBackGroundServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ImportArchiveBackgroundTaskQueue>();
            serviceCollection.AddHostedService<ArchiveImportHostedService>();
        }
    }
}
