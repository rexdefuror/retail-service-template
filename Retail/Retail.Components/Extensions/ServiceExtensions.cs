using Microsoft.Extensions.DependencyInjection;
using Retail.Components.TemplateEvent.Commands;
using Retail.Components.TemplateEvent.Queries;

namespace Retail.Components.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureComponents(this IServiceCollection services)
        {
            services.AddTransient<StoreContentCommandHandler>();
            services.AddTransient<FetchContentQueryHandler>();

            return services;
        }
    }
}
