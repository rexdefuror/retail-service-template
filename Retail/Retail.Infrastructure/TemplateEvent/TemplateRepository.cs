using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Retail.Persistence.Database;
using System;
using System.Threading.Tasks;

namespace Retail.Infrastructure.TemplateEvent
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IDbConnectionFactory _factory;
        private readonly IMemoryCache _cache;
        private readonly TemplateRepositoryOptions _options;

        /// <summary>Initializes a new instance of the <see cref="TemplateRepository" /> class.</summary>
        /// <param name="factory">The factory.</param>
        /// <param name="cache">The cache.</param>
        public TemplateRepository(IDbConnectionFactory factory, IMemoryCache cache, IOptionsMonitor<TemplateRepositoryOptions> options)
        {
            _factory = factory;
            _cache = cache;
            _options = options.CurrentValue;
        }

        /// <summary>Gets Content by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns cref="Content">Content</returns>
        public async Task<Content> GetById(int id)
        {
            var key = typeof(Content).Name + id;

            if (!_cache.TryGetValue<Content>(key, out var result))
            {
                using (var db = _factory.Create())
                {
                    result = await db.QueryFirstOrDefaultAsync<Content>(@"
                    SELECT
                        *
                    FROM 
                        Contents
                    WHERE
                        Id = @id
                    ", new { id });
                }

                if (result != null)
                {
                    _cache.Set(key, result, TimeSpan.FromSeconds(_options.CacheSeconds));
                }
            }

            return result;
        }

        public async Task Store(string text, DateTime addedOn)
        {
            using (var db = _factory.Create())
            {
                await db.ExecuteAsync(@"
                    INSERT INTO Contents
                        ( Text, AddedOn )
                    VALUES
                        ( @text, @addedOn )
                ", new { text, addedOn });
            }
        }
    }
}
