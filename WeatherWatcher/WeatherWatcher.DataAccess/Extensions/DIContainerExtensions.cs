using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherWatcher.DataAccess.EFCore;
using WeatherWatcher.DataAccess.Repositories;
using WeatherWatcher.Services.Infrastructure.Repositories;
using WeatherWatcher.Services.Infrastructure.Services;

namespace WeatherWatcher.DataAccess.Extensions
{
    public static class DIContainerExtensions
    {
        public static void AddDbContext(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWeatherInfoRepository, WeatherInfoRepository>();
            serviceCollection.AddScoped<ILoadArchiveStatusRepository, LoadArchiveStatusRepository>();
        }
    }
}
