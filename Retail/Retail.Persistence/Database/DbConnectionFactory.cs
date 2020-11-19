using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Retail.Persistence.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseOptions _options;

        public DbConnectionFactory(IOptionsMonitor<DatabaseOptions> options)
        {
            _options = options.CurrentValue;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_options.ConnectionString);
        }
    }
}
