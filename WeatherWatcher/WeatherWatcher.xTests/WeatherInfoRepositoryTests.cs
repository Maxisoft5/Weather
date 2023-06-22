using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatcher;
using WeatherWatcher.DataAccess.EFCore;
using WeatherWatcher.xTests.Wrappers;

namespace WeatherWatcher.xTests
{
    public class WeatherInfoRepositoryTests : IClassFixture<WebApplicationFactoryTest<Program>>
    {
        private readonly WebApplicationFactoryTest<Program> _factory;

        public WeatherInfoRepositoryTests(WebApplicationFactoryTest<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DbContextTest()
        {
            // arrange
            DataContext dataContext = null;

            // act
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                dataContext = scopedServices.GetRequiredService<DataContext>();

            }

            // assert
            dataContext.ShouldNotBeNull();

        }


    }
}
