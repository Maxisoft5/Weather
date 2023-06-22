using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shouldly;
using WeatherWatcher.DataAccess.EFCore;
using WeatherWatcher.DataAccess.Extensions;
using WeatherWatcher.Tests.WeatherInfo.Wrappers;

namespace WeatherWatcher.Tests.WeatherInfo
{
    public class WebAppBuilderTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _webApplicationFactory { get; set; }

        [SetUp]
        public void Setup()
        {
          
        }


        [Test]
        public void DIDbContextRegistryTest()
        {
            var context = _webApplicationFactory.Services;
            context.ShouldNotBeNull();
        }

        [Test]
        public void DIRepositoriesRegistryTest()
        {
        }

        [Test]
        public void DIServicesRegistryTest()
        {

        }
    }
}
