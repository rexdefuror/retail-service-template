using Microsoft.Extensions.Logging;
using Retail.Infrastructure.TemplateEvent;
using System;
using System.Threading.Tasks;

namespace Retail.Components.TemplateEvent.Queries
{
    public class FetchContentQuery
    {
        public FetchContentQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class FetchContentQueryHandler
    {
        private readonly ITemplateRepository _repository;
        private readonly ILogger<FetchContentQuery> _logger;

        public FetchContentQueryHandler(ITemplateRepository repository, ILogger<FetchContentQuery> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Content> Handle(FetchContentQuery query)
        {
            // mapping to a DTO would happen here

            try
            {
                var content = await _repository.GetById(query.Id);
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
