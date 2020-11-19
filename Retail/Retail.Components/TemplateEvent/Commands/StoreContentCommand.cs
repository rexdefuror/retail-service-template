using Microsoft.Extensions.Logging;
using Retail.Infrastructure.TemplateEvent;
using System;
using System.Threading.Tasks;

namespace Retail.Components.TemplateEvent.Commands
{
    public class StoreContentCommand
    {
        public StoreContentCommand(string text, DateTimeOffset addedOn)
        {
            Text = text;
            AddedOn = addedOn;
        }

        public string Text { get; set; }
        public DateTimeOffset AddedOn { get; set; }
    }

    public class StoreContentCommandHandler
    {
        private readonly ILogger<StoreContentCommand> _logger;
        private readonly ITemplateRepository _templateRepository;

        public StoreContentCommandHandler(ILogger<StoreContentCommand> logger, ITemplateRepository templateRepository)
        {
            _logger = logger;
            _templateRepository = templateRepository;
        }

        public async Task Handle(StoreContentCommand command)
        {
            try
            {
                await _templateRepository.Store(command.Text, command.AddedOn.UtcDateTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
