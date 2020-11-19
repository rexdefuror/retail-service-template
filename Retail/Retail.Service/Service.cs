using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Retail.Components.Consumers;
using Retail.Service.Options;
using System;
using System.Threading.Tasks;

namespace Retail.Service
{
    public class Service
    {
        private readonly MassTransitOptions _massTransitOptions;
        private readonly ILogger<Service> _logger;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>Initializes a new instance of the <see cref="Service" /> class.</summary>
        /// <param name="massTransitOptionsMonitor">The mass transit options monitor.</param>
        /// <param name="logger">The logger.</param>
        public Service(IOptionsMonitor<MassTransitOptions> massTransitOptionsMonitor, ILogger<Service> logger, IServiceProvider serviceProvider)
        {
            _massTransitOptions = massTransitOptionsMonitor.CurrentValue;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            _logger.LogInformation("Starting service");
            try
            {
                var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    sbc.Host(_massTransitOptions.RabbitHost);
                    sbc.ReceiveEndpoint(_massTransitOptions.RabbitQueue, e =>
                    {
                        e.Consumer<TemplateEventConsumer>(_serviceProvider);
                    });
                });

                await bus.StartAsync();
            }
            // Adding more meaningful messages and exceptions is recommended.
            catch (MassTransitException massTransitException)
            {
                _logger.LogError(massTransitException.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }

}
