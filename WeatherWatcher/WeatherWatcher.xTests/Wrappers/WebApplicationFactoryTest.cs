using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DataAccess.Extensions;
using WeatherWatcher.Services.Extensions;

namespace WeatherWatcher.xTests.Wrappers
{
    public class WebApplicationFactoryTest<TProgram>
            : WebApplicationFactory<TProgram> where TProgram : class
    {
        private IConfiguration Configuration { set; get; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((host, config) =>
            {
                Configuration = config.Build();
            });
            builder.ConfigureServices(services =>
            {
                var connection = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext(connection);
                services.AddRepositories();
                services.AddServices();
            });
            
        }
    }
}
