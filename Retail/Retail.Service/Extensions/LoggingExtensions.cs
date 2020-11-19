using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace Retail.Service.Extensions
{
    public static class LoggingExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration().ReadFrom.Configuration(configuration);
            Log.Logger = loggerConfig.CreateLogger();

            services.AddLogging(configure => configure.AddSerilog());

            return services;
        }
    }
}
