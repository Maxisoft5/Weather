using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher.DataAccess.Extensions;
using WeatherWatcher.Services.Extensions;

namespace WeatherWatcher.Tests.WeatherInfo.Wrappers
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private IConfiguration _configuration;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
          
        }

        public IConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
