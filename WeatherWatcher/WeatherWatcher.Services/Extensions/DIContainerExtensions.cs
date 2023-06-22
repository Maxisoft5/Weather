using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.Services.Infrastructure.Services;
using WeatherWatcher.Services.Services;

namespace WeatherWatcher.Services.Extensions
{
    public static class DIContainerExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoadArchiveStatusStervice, LoadArchiveStatusStervice>();
            serviceCollection.AddScoped<IWeatherInfoService, WeatherInfoService>();
        }
    }
}
