using MassTransit;
using Microsoft.Extensions.Logging;
using Retail.Components.TemplateEvent.Commands;
using Retail.Contracts;
using System.Threading.Tasks;

namespace Retail.Components.Consumers
{
    public class TemplateEventConsumer : IConsumer<TemplateEventOccurred>
    {
        private readonly ILogger<TemplateEventConsumer> _logger;
        private readonly StoreContentCommandHandler _commandHandler;

        public TemplateEventConsumer(ILogger<TemplateEventConsumer> logger, StoreContentCommandHandler commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        public async Task Consume(ConsumeContext<TemplateEventOccurred> context)
        {
            _logger.LogInformation("New content submitted.");
            if (context.TryGetMessage(out context))
            {
                var command = new StoreContentCommand(context.Message.Content, context.Message.Created);
                await _commandHandler.Handle(command);
            }
            else
            {
                _logger.LogWarning("Invalid message.");
            }
        }
    }
}
