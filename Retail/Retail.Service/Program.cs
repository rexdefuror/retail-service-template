using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retail.Components.Extensions;
using Retail.Infrastructure.TemplateEvent;
using Retail.Persistence.Database;
using Retail.Service.Extensions;
using Retail.Service.Options;
using System;
using System.Threading.Tasks;

namespace Retail.Service
{
    class Program
    {
        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        private static IConfiguration Configuration { get; set; }

        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<Service>().Run();
            await Task.Run(() => Console.ReadKey());
        }

        /// <summary>Configures the services.</summary>
        /// <returns cref="IServiceCollection">
        ///   The service collection.
        /// </returns>
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.ConfigureLogging(Configuration);
            services.AddMemoryCache();
            services.AddOptions();

            services.Configure<TemplateRepositoryOptions>(Configuration.GetSection(TemplateRepositoryOptions.Section));
            services.AddTransient<ITemplateRepository, TemplateRepository>();

            services.Configure<DatabaseOptions>(Configuration.GetSection(DatabaseOptions.Section));
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

            services.ConfigureComponents();

            services.Configure<MassTransitOptions>(Configuration.GetSection(MassTransitOptions.Section));

            // IMPORTANT! Do not remove registration of the service entry point.
            services.AddTransient<Service>();
            return services;
        }
    }
}
