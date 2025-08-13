using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRESERVICA.NASA.DEMO.Drivers;
using PRESERVICA.NASA.DEMO.Pages;
using PRESERVICA.NASA.DEMO.Services;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace PRESERVICA.NASA.DEMO
{
    public static class TestStartup
    {
        [ScenarioDependencies]
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration config = BuildConfigurationSettings();
            
            services.AddSingleton(config);
            // Register PlaywrightManager
            services.AddScoped<PlaywrightManager>();
            services.AddScoped<IPageService, PageService>();
            // Register SignUpPage
            services.AddTransient<SignUpPage>();
            return services;
        }

        private static IConfiguration BuildConfigurationSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}